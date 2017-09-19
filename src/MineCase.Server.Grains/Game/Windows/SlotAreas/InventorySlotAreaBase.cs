using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal abstract class InventorySlotAreaBase : SlotArea
    {
        protected int OffsetInContainer { get; }

        public InventorySlotAreaBase(int slotsCount, int offsetInContainer, WindowGrain window, IGrainFactory grainFactory)
            : base(slotsCount, window, grainFactory)
        {
            OffsetInContainer = offsetInContainer;
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return player.Ask(new AskSlot { Index = slotIndex + OffsetInContainer });
        }

        public override async Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            await player.Tell(new SetSlot { Index = slotIndex + OffsetInContainer, Slot = slot });
            await NotifySlotChanged(player, slotIndex, slot);
        }
    }
}
