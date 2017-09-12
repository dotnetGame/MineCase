using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class FurnaceSlotArea : SlotArea
    {
        public const int FurnaceSlotsCount = 3;

        private readonly IFurnaceBlockEntity _furnaceEntity;

        public FurnaceSlotArea(IFurnaceBlockEntity furnaceEntity, WindowGrain window, IGrainFactory grainFactory)
            : base(FurnaceSlotsCount, window, grainFactory)
        {
            _furnaceEntity = furnaceEntity;
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return _furnaceEntity.GetSlot(slotIndex);
        }

        public override async Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            await _furnaceEntity.SetSlot(slotIndex, slot);
            await BroadcastSlotChanged(slotIndex, slot);
        }
    }
}
