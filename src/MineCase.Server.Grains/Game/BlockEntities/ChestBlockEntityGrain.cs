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
using MineCase.Server.Game.Entities.Components;

namespace MineCase.Server.Game.BlockEntities
{
    [Reentrant]
    internal class ChestBlockEntityGrain : BlockEntityGrain, IChestBlockEntity
    {
        private Slot[] _slots;
        private IChestBlockEntity _neightborEntity;

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
            var masterEntity = FindMasterEntity(_neightborEntity);
            if (masterEntity.GetPrimaryKeyString() == this.GetPrimaryKeyString())
            {
                if (_chestWindow == null)
                {
                    _chestWindow = GrainFactory.GetGrain<IChestWindow>(Guid.NewGuid());
                    await _chestWindow.SetEntities((_neightborEntity == null ?
                        new[] { this.AsReference<IChestBlockEntity>() } : new[] { this.AsReference<IChestBlockEntity>(), _neightborEntity }).AsImmutable());
                }

                await player.Tell(new OpenWindow { Window = _chestWindow });
            }
            else
            {
                await masterEntity.UseBy(player);
            }
        }

        public override async Task Destroy()
        {
            if (_chestWindow != null)
                await _chestWindow.Destroy();
        }

        public async Task ClearNeighborEntity()
        {
            _neightborEntity = null;
            if (_chestWindow != null)
            {
                await _chestWindow.Destroy();
                await _chestWindow.SetEntities(new[] { this.AsReference<IChestBlockEntity>() }.AsImmutable());
            }
        }

        public async Task SetNeighborEntity(IChestBlockEntity chestEntity)
        {
            _neightborEntity = chestEntity;
            if (_chestWindow != null)
            {
                await _chestWindow.Destroy();
                await _chestWindow.SetEntities(new[] { this.AsReference<IChestBlockEntity>(), chestEntity }.AsImmutable());
            }
        }

        private IChestBlockEntity FindMasterEntity(IChestBlockEntity neighborEntity)
        {
            if (neighborEntity == null)
                return this.AsReference<IChestBlockEntity>();

            // 按 X, Z 排序取最小
            return (from e in new[] { this.AsReference<IChestBlockEntity>(), neighborEntity }
                    let pos = e.GetBlockEntityPosition()
                    orderby pos.X, pos.Z
                    select e).First();
        }
    }
}
