using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using Orleans;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class FurnaceSlotArea : SlotArea
    {
        public const int FurnaceSlotsCount = 3;

        private readonly IBlockEntity _furnaceEntity;

        public FurnaceSlotArea(IBlockEntity furnaceEntity, WindowGrain window, IGrainFactory grainFactory)
            : base(FurnaceSlotsCount, window, grainFactory)
        {
            _furnaceEntity = furnaceEntity;
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return _furnaceEntity.Ask(new AskSlot { Index = slotIndex });
        }

        public override async Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            await _furnaceEntity.Tell(new SetSlot { Index = slotIndex, Slot = slot });
            await BroadcastSlotChanged(slotIndex, slot);
        }
    }
}
