using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MineCase.Server.Game.Entities;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal abstract class SlotArea
    {
        public const int ArmorSlotsCount = 4;
        public const int InventorySlotsCount = 27;
        public const int HotbarSlotsCount = 9;
        public const int OffhandSlotsCount = 1;

        public const int UserSlotsCount = ArmorSlotsCount + InventorySlotsCount + HotbarSlotsCount + OffhandSlotsCount;

        public const int ArmorOffsetInContainer = 0;
        public const int InventoryOffsetInContainer = ArmorOffsetInContainer + ArmorSlotsCount;
        public const int HotbarOffsetInContainer = InventoryOffsetInContainer + InventorySlotsCount;
        public const int OffhandOffsetInContainer = HotbarOffsetInContainer + HotbarSlotsCount;

        public int SlotsCount { get; }

        protected WindowGrain Window { get; }

        protected IGrainFactory GrainFactory { get; }

        public SlotArea(int slotsCount, WindowGrain window, IGrainFactory grainFactory)
        {
            SlotsCount = slotsCount;
            Window = window;
            GrainFactory = grainFactory;
        }

        public abstract Task<Slot> GetSlot(IPlayer player, int slotIndex);

        public abstract Task SetSlot(IPlayer player, int slotIndex, Slot slot);

        protected const byte MaxStackCount = 64;

        public virtual async Task<Slot> DistributeStack(IPlayer player, Slot item, bool canUseEmptySlot, bool fillFromBack)
        {
            for (int i = 0; i < SlotsCount && !item.IsEmpty; i++)
            {
                int slotIndex = fillFromBack ? (SlotsCount - 1 - i) : i;

                var targetSlot = await GetSlot(player, slotIndex);
                if (canUseEmptySlot && targetSlot.IsEmpty)
                {
                    await SetSlot(player, slotIndex, item);
                    return Slot.Empty;
                }
                else if (TryStackSlot(ref item, ref targetSlot))
                {
                    await SetSlot(player, slotIndex, targetSlot);
                }
            }

            return item;
        }

        protected void NotifySlotChanged(IPlayer player, int slotIndex, Slot item)
        {
            Window.NotifySlotChanged(this, player, slotIndex, item);
        }

        public virtual async Task Click(IPlayer player, int slotIndex, ClickAction clickAction, Slot clickedItem)
        {
            var slot = await GetSlot(player, slotIndex);
            var draggedSlot = await player.GetDraggedSlot();
            switch (clickAction)
            {
                case ClickAction.LeftMouseClick:
                    if (draggedSlot.IsEmpty || !draggedSlot.CanStack(slot))
                    {
                        // 交换
                        await SetSlot(player, slotIndex, draggedSlot);
                        await player.SetDraggedSlot(slot);
                    }
                    else if (TryStackSlot(ref draggedSlot, ref slot))
                    {
                        // 堆叠到最大
                        await SetSlot(player, slotIndex, slot);
                        await player.SetDraggedSlot(draggedSlot);
                    }

                    break;
                case ClickAction.RightMouseClick:
                    if (draggedSlot.IsEmpty)
                    {
                        // 取一半
                        if (!slot.IsEmpty)
                        {
                            var takeCount = (byte)Math.Ceiling(slot.ItemCount / 2.0f);
                            draggedSlot = slot.WithItemCount(takeCount);
                            slot.ItemCount -= takeCount;
                            slot.MakeEmptyIfZero();

                            await SetSlot(player, slotIndex, slot);
                            await player.SetDraggedSlot(draggedSlot);
                        }
                    }
                    else if (slot.IsEmpty || draggedSlot.CanStack(slot))
                    {
                        // 放一个
                        if (draggedSlot.ItemCount > 0 && (slot.IsEmpty || slot.ItemCount < MaxStackCount))
                        {
                            draggedSlot.ItemCount--;
                            if (slot.IsEmpty)
                                slot = draggedSlot.WithItemCount(1);
                            else
                                slot.ItemCount++;
                            draggedSlot.MakeEmptyIfZero();

                            await SetSlot(player, slotIndex, slot);
                            await player.SetDraggedSlot(draggedSlot);
                        }
                    }
                    else
                    {
                        // 交换
                        await SetSlot(player, slotIndex, draggedSlot);
                        await player.SetDraggedSlot(slot);
                    }

                    break;
                default:
                    break;
            }
        }

        protected bool TryStackSlot(ref Slot source, ref Slot target)
        {
            if (target.ItemCount <= MaxStackCount && target.CanStack(source))
            {
                var toStack = (byte)Math.Min(source.ItemCount, MaxStackCount - target.ItemCount);
                target.ItemCount += toStack;
                source.ItemCount -= toStack;
                source.MakeEmptyIfZero();
                return true;
            }

            return false;
        }

        public virtual Task Close(IPlayer player)
        {
            return Task.CompletedTask;
        }
    }
}
