using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class ChestSlotArea : SlotArea
    {
        public const int ChestSlotsCount = 9 * 3;

        private readonly IChestBlockEntity _chestEntity;

        public ChestSlotArea(IChestBlockEntity chestEntity, WindowGrain window, IGrainFactory grainFactory)
            : base(ChestSlotsCount, window, grainFactory)
        {
            _chestEntity = chestEntity;
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return _chestEntity.GetSlot(slotIndex);
        }

        public override async Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            await _chestEntity.SetSlot(slotIndex, slot);
            await BroadcastSlotChanged(slotIndex, slot);
        }
    }
}
