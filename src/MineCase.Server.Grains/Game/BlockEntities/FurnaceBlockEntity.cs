using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows;
using MineCase.Server.Game.Windows.SlotAreas;
using MineCase.Server.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.BlockEntities
{
    [Reentrant]
    internal class FurnaceBlockEntity : BlockEntityGrain, IFurnaceBlockEntity
    {
        private Slot[] _slots;

        public override Task OnActivateAsync()
        {
            _slots = Enumerable.Repeat(Slot.Empty, FurnaceSlotArea.FurnaceSlotsCount).ToArray();
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

        private IFurnaceWindow _furnaceWindow;

        public async Task UseBy(IPlayer player)
        {
            if (_furnaceWindow == null)
            {
                _furnaceWindow = GrainFactory.GetGrain<IFurnaceWindow>(Guid.NewGuid());
                await _furnaceWindow.SetEntity(this);
            }

            await player.OpenWindow(_furnaceWindow);
        }

        public override async Task OnCreated()
        {
            var chunkPos = Position.ToChunkWorldPos();
            var tracker = GrainFactory.GetGrain<IChunkTrackingHub>(World.MakeChunkTrackingHubKey(chunkPos.X, chunkPos.Z));
            await tracker.Subscribe(this);
        }

        public override async Task Destroy()
        {
            var chunkPos = Position.ToChunkWorldPos();
            var tracker = GrainFactory.GetGrain<IChunkTrackingHub>(World.MakeChunkTrackingHubKey(chunkPos.X, chunkPos.Z));
            await tracker.Unsubscribe(this);
        }

        public async Task OnGameTick(TimeSpan deltaTime)
        {
            if (_furnaceWindow != null)
                await _furnaceWindow.OnGameTick(deltaTime);
        }
    }
}
