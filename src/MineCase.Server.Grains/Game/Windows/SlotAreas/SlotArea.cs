using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.World;
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

        public virtual async Task TryUseItem(IPlayer player, int slotIndex)
        {
            var slot = await GetSlot(player, slotIndex);
            if (!slot.IsEmpty && slot.ItemCount >= 1)
            {
                slot.ItemCount--;
                slot.MakeEmptyIfZero();
                await SetSlot(player, slotIndex, slot);
            }
        }

        protected Task NotifySlotChanged(IPlayer player, int slotIndex, Slot item)
        {
            return Window.NotifySlotChanged(this, player, slotIndex, item);
        }

        protected Task BroadcastSlotChanged(int slotIndex, Slot item)
        {
            return Window.BroadcastSlotChanged(this, slotIndex, item);
        }

        public virtual async Task Click(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex, ClickAction clickAction, Slot clickedItem)
        {
            switch (clickAction)
            {
                case ClickAction.LeftMouseClick:
                    await OnLeftMouseClick(player, slotIndex);
                    break;
                case ClickAction.RightMouseClick:
                    await OnRightMouseClick(player, slotIndex);
                    break;
                case ClickAction.ShiftLeftMouseClick:
                case ClickAction.ShiftRightMouseClick:
                    await OnMouseShiftClick(player, slotIndex);
                    break;
                case ClickAction.DropKey:
                case ClickAction.CtrlDropKey:
                    await OnDropClick(player, slotIndex, clickAction == ClickAction.CtrlDropKey);
                    break;
                case ClickAction.NumberKey1:
                case ClickAction.NumberKey2:
                case ClickAction.NumberKey3:
                case ClickAction.NumberKey4:
                case ClickAction.NumberKey5:
                case ClickAction.NumberKey6:
                case ClickAction.NumberKey7:
                case ClickAction.NumberKey8:
                case ClickAction.NumberKey9:
                    await OnNumberKeyClick(player, slotIndex, clickAction - ClickAction.NumberKey1);
                    break;
                case ClickAction.LeftMouseDragBegin:
                    await OnLeftMouseDragBegin(player, slotAreas, globalIndex, slotIndex);
                    break;
                case ClickAction.LeftMouseAddSlot:
                    await OnLeftMouseAddSlot(player, slotAreas, globalIndex, slotIndex);
                    break;
                case ClickAction.LeftMouseDragEnd:
                    await OnLeftMouseDragEnd(player, slotAreas, globalIndex, slotIndex);
                    break;

                case ClickAction.RightMouseDragBegin:
                    await OnRightMouseDragBegin(player, slotAreas, globalIndex, slotIndex);
                    break;
                case ClickAction.RightMouseAddSlot:
                    await OnRightMouseAddSlot(player, slotAreas, globalIndex, slotIndex);
                    break;
                case ClickAction.RightMouseDragEnd:
                    await OnRightMouseDragEnd(player, slotAreas, globalIndex, slotIndex);
                    break;

                case ClickAction.DoubleClick:
                    await OnDoubleClick(player, slotAreas, globalIndex, slotIndex);
                    break;
                default:
                    await Window.BroadcastWholeWindow();
                    break;
            }
        }

        private async Task OnNumberKeyClick(IPlayer player, int slotIndex, int numberIndex)
        {
            var inventory = await player.Ask(AskInventoryWindow.Default);
            var hotbarIndex = await inventory.GetHotbarGlobalIndex(player, numberIndex);
            var hotbarSlot = await inventory.GetSlot(player, hotbarIndex);
            var slot = await GetSlot(player, slotIndex);

            // 交换
            await inventory.SetSlot(player, hotbarIndex, slot);
            await SetSlot(player, slotIndex, hotbarSlot);
        }

        private async Task OnDropClick(IPlayer player, int slotIndex, bool dropWholeSlot)
        {
            var slot = await GetSlot(player, slotIndex);
            if (!slot.IsEmpty)
            {
                var position = await player.GetPosition();
                var chunk = position.ToChunkWorldPos();
                var world = await player.GetWorld();
                var finder = GrainFactory.GetPartitionGrain<ICollectableFinder>(world, chunk);

                if (dropWholeSlot)
                {
                    await player.Tell(new TossPickup { Slots = new[] { slot } });
                    await SetSlot(player, slotIndex, Slot.Empty);
                }
                else
                {
                    await player.Tell(new TossPickup { Slots = new[] { slot.CopyOne() } });
                    slot.ItemCount--;
                    slot.MakeEmptyIfZero();
                    await SetSlot(player, slotIndex, slot);
                }
            }
        }

        protected virtual async Task OnMouseShiftClick(IPlayer player, int slotIndex)
        {
            var slot = await GetSlot(player, slotIndex);
            slot = await Window.DistributeStack(player, slot);
            await SetSlot(player, slotIndex, slot);
        }

        protected virtual async Task OnRightMouseClick(IPlayer player, int slotIndex)
        {
            var slot = await GetSlot(player, slotIndex);
            var draggedSlot = await player.Ask(AskDraggedSlot.Default);

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
                    await player.Tell(new SetDraggedSlot { Slot = draggedSlot });
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
                    await player.Tell(new SetDraggedSlot { Slot = draggedSlot });
                }
            }
            else
            {
                // 交换
                await SetSlot(player, slotIndex, draggedSlot);
                await player.Tell(new SetDraggedSlot { Slot = slot });
            }
        }

        protected virtual async Task OnLeftMouseClick(IPlayer player, int slotIndex)
        {
            var slot = await GetSlot(player, slotIndex);
            var draggedSlot = await player.Ask(AskDraggedSlot.Default);

            if (draggedSlot.IsEmpty || !draggedSlot.CanStack(slot))
            {
                // 交换
                await SetSlot(player, slotIndex, draggedSlot);
                await player.Tell(new SetDraggedSlot { Slot = slot });
            }
            else if (TryStackSlot(ref draggedSlot, ref slot))
            {
                // 堆叠到最大
                await SetSlot(player, slotIndex, slot);
                await player.Tell(new SetDraggedSlot { Slot = draggedSlot });
            }
        }

        protected virtual async Task OnLeftMouseDragBegin(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex)
        {
            // begin left mouse drag, clear player drag path data
            await player.Tell(new SetDraggedPath { Path = new List<int>() });
        }

        protected virtual async Task OnLeftMouseAddSlot(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex)
        {
            var slot = await GetSlot(player, slotIndex);
            var draggedSlot = await player.Ask(AskDraggedSlot.Default);
            var dragSlotPath = await player.Ask(AskDraggedPath.Default);

            if (slot.IsEmpty || slot.BlockId == draggedSlot.BlockId)
            {
                dragSlotPath.Add(globalIndex);
            }

            await player.Tell(new SetDraggedPath { Path = dragSlotPath });
        }

        protected virtual async Task OnLeftMouseDragEnd(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex)
        {
            var draggedSlot = await player.Ask(AskDraggedSlot.Default);
            var dragSlotPath = await player.Ask(AskDraggedPath.Default);
            var slot = draggedSlot;
            slot.ItemCount = 0;

            int leftNum = draggedSlot.ItemCount;
            int averageNum = draggedSlot.ItemCount / dragSlotPath.Count;
            foreach (var eachSlotIndex in dragSlotPath)
            {
                var localSlot = GlobalSlotIndexToLocal(slotAreas, eachSlotIndex);
                var eachSlot = await localSlot.slotArea.GetSlot(player, localSlot.slotIndex);

                slot.ItemCount = (byte)Math.Min(MaxStackCount, eachSlot.ItemCount + averageNum);

                await localSlot.slotArea.SetSlot(player, localSlot.slotIndex, slot);

                if (eachSlot.IsEmpty)
                {
                    leftNum -= Math.Min(MaxStackCount - 0, averageNum);
                }
                else
                {
                    leftNum -= Math.Min(MaxStackCount - eachSlot.ItemCount, averageNum);
                }
            }

            // now set left item for that slot is full
            slot.ItemCount = (byte)leftNum;
            slot.MakeEmptyIfZero();
            await player.Tell(new SetDraggedSlot { Slot = slot });

            // last, clear dragged path
            await player.Tell(new SetDraggedPath { Path = new List<int>() });
        }

        protected virtual async Task OnRightMouseDragBegin(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex)
        {
            await player.Tell(new SetDraggedPath { Path = new List<int>() });
        }

        protected virtual async Task OnRightMouseAddSlot(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex)
        {
            var slot = await GetSlot(player, slotIndex);
            var draggedSlot = await player.Ask(AskDraggedSlot.Default);
            var dragSlotPath = await player.Ask(AskDraggedPath.Default);

            if (slot.IsEmpty || slot.BlockId == draggedSlot.BlockId)
            {
                dragSlotPath.Add(globalIndex);
            }

            await player.Tell(new SetDraggedPath { Path = dragSlotPath });
        }

        protected virtual async Task OnRightMouseDragEnd(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex)
        {
            var draggedSlot = await player.Ask(AskDraggedSlot.Default);
            var dragSlotPath = await player.Ask(AskDraggedPath.Default);
            var slot = draggedSlot;
            slot.ItemCount = 0;

            int leftNum = draggedSlot.ItemCount;
            int averageNum = 1;
            foreach (var eachSlotIndex in dragSlotPath)
            {
                var localSlot = GlobalSlotIndexToLocal(slotAreas, eachSlotIndex);
                var eachSlot = await localSlot.slotArea.GetSlot(player, localSlot.slotIndex);

                slot.ItemCount = (byte)Math.Min(MaxStackCount, eachSlot.ItemCount + averageNum);

                await localSlot.slotArea.SetSlot(player, localSlot.slotIndex, slot);

                if (eachSlot.IsEmpty)
                {
                    leftNum -= Math.Min(MaxStackCount - 0, averageNum);
                }
                else
                {
                    leftNum -= Math.Min(MaxStackCount - eachSlot.ItemCount, averageNum);
                }
            }

            // now set left item for that slot is full
            slot.ItemCount = (byte)leftNum;
            slot.MakeEmptyIfZero();
            await player.Tell(new SetDraggedSlot { Slot = slot });

            // last, clear dragged path
            await player.Tell(new SetDraggedPath { Path = new List<int>() });
        }

        protected virtual async Task OnDoubleClick(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex)
        {
            var slot = await GetSlot(player, slotIndex);
            var draggedSlot = await player.Ask(AskDraggedSlot.Default);
            short blockId = -1;

            if (draggedSlot.BlockId != -1) blockId = draggedSlot.BlockId;
            else if (slot.BlockId != -1) blockId = slot.BlockId;

            int dragCount = draggedSlot.ItemCount;

            foreach (var eachSlotArea in slotAreas)
            {
                for (int eachSlotIndex = 0; eachSlotIndex < eachSlotArea.SlotsCount; ++eachSlotIndex)
                {
                    var eachSlot = await eachSlotArea.GetSlot(player, eachSlotIndex);
                    if (eachSlot.BlockId == blockId && eachSlot.ItemCount != 0)
                    {
                        // This count means to move how many block to your hand
                        int moveCount = Math.Min(MaxStackCount - dragCount, eachSlot.ItemCount);
                        eachSlot.ItemCount -= (byte)moveCount;
                        dragCount += moveCount;
                        await eachSlotArea.SetSlot(player, eachSlotIndex, eachSlot);
                        if (dragCount >= MaxStackCount)
                        {
                            await player.Tell(new SetDraggedSlot
                            {
                                Slot = new Slot { ItemCount = (byte)dragCount, BlockId = blockId }
                            });
                            return;
                        }
                    }
                }
            }

            await player.Tell(new SetDraggedSlot
            {
                Slot = new Slot { ItemCount = (byte)dragCount, BlockId = blockId }
            });
            return;
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

        // TODO merge method in games/windows/WindowGrain.cs
        protected (SlotArea slotArea, int slotIndex) GlobalSlotIndexToLocal(List<SlotArea> slotAreas, int slotIndex)
        {
            for (int i = 0; i < slotAreas.Count; i++)
            {
                if (slotIndex < slotAreas[i].SlotsCount)
                    return (slotAreas[i], slotIndex);
                slotIndex -= slotAreas[i].SlotsCount;
            }

            throw new ArgumentOutOfRangeException(nameof(slotIndex));
        }

        public virtual Task Close(IPlayer player)
        {
            return Task.CompletedTask;
        }
    }
}
