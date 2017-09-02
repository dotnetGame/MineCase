using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Entities;
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

        private readonly WindowGrain _window;

        public SlotArea(int slotsCount, WindowGrain window)
        {
            SlotsCount = slotsCount;
            _window = window;
        }

        public abstract Task<Slot> GetSlot(IPlayer player, int slotIndex);

        public abstract Task SetSlot(IPlayer player, int slotIndex, Slot slot);

        private const byte MaxStackCount = 64;

        public virtual async Task<Slot> DistributeStack(IPlayer player, Slot item, bool fillFromBack)
        {
            for (int i = 0; i < SlotsCount && !item.IsEmpty; i++)
            {
                int slotIndex = fillFromBack ? (SlotsCount - 1 - i) : i;

                var targetSlot = await GetSlot(player, slotIndex);
                if (targetSlot.IsEmpty)
                {
                    await SetSlot(player, slotIndex, item);
                    return Slot.Empty;
                }
                else if (targetSlot.ItemCount < MaxStackCount && targetSlot.CanStack(item))
                {
                    var toStack = (byte)Math.Min(item.ItemCount, MaxStackCount - targetSlot.ItemCount);
                    targetSlot.ItemCount += toStack;
                    item.ItemCount -= toStack;
                    await SetSlot(player, slotIndex, targetSlot);
                }
            }

            return item;
        }

        protected void NotifySlotChanged(IPlayer player, int slotIndex, Slot item)
        {
            _window.NotifySlotChanged(this, player, slotIndex, item);
        }
    }
}
