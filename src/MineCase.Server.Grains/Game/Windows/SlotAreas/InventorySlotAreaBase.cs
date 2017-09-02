using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Entities;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal abstract class InventorySlotAreaBase : SlotArea
    {
        protected int OffsetInContainer { get; }

        public InventorySlotAreaBase(int slotsCount, int offsetInContainer, WindowGrain window)
            : base(slotsCount, window)
        {
            OffsetInContainer = offsetInContainer;
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return player.GetInventorySlot(slotIndex + OffsetInContainer);
        }

        public override async Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            await player.SetInventorySlot(slotIndex + OffsetInContainer, slot);
            NotifySlotChanged(player, slotIndex, slot);
        }
    }
}
