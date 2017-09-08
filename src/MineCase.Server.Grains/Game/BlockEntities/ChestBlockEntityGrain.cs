using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows;
using MineCase.Server.Game.Windows.SlotAreas;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities
{
    internal class ChestBlockEntityGrain : BlockEntityGrain, IChestBlockEntity
    {
        private Slot[] _slots;

        public override Task OnActivateAsync()
        {
            _slots = Enumerable.Repeat(Slot.Empty, ChestSlotArea.ChestSlotsCount).ToArray();
            return base.OnActivateAsync();
        }

        public Task<Slot> GetSlot(int slotIndex)
        {
            return Task.FromResult(_slots[slotIndex]);
        }

        public Task SetSlot(int slotIndex, Slot item)
        {
            _slots[slotIndex] = item;
            return Task.CompletedTask;
        }

        private IChestWindow _chestWindow;

        public async Task UseBy(IPlayer player)
        {
            if (_chestWindow == null)
            {
                _chestWindow = GrainFactory.GetGrain<IChestWindow>(Guid.NewGuid());
                await _chestWindow.SetEntities(new[] { this.AsReference<IChestBlockEntity>() }.AsImmutable());
            }

            await _chestWindow.OpenWindow(player);
        }

        public override async Task Destroy()
        {
            if (_chestWindow != null)
                await _chestWindow.Destroy();
        }
    }
}
