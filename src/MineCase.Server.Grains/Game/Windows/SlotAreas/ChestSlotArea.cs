using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using Orleans;
using MineCase.Engine;
using MineCase.Server.Game.Entities.Components;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class ChestSlotArea : SlotArea
    {
        public const int ChestSlotsCount = 9 * 3;

        private readonly IDependencyObject _chestEntity;

        public ChestSlotArea(IDependencyObject chestEntity, WindowGrain window, IGrainFactory grainFactory)
            : base(ChestSlotsCount, window, grainFactory)
        {
            _chestEntity = chestEntity;
        }

        public override Task<Slot> GetSlot(IPlayer player, int slotIndex)
        {
            return _chestEntity.Ask(new AskSlot { Index = slotIndex });
        }

        public override async Task SetSlot(IPlayer player, int slotIndex, Slot slot)
        {
            await _chestEntity.Tell(new SetSlot { Index = slotIndex, Slot = slot });
            await BroadcastSlotChanged(slotIndex, slot);
        }
    }
}
