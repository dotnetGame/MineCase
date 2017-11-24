using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [PersistTableName("worldPartition")]
    [Reentrant]
    internal class WorldPartitionGrain : AddressByPartitionGrain, IWorldPartition
    {
        private ITickEmitter _tickEmitter;
        private ICollectableFinder _collectableFinder;
        private IChunkTrackingHub _chunkTrackingHub;
        private IChunkColumn _chunkColumn;
        private AutoSaveStateComponent _autoSave;
        private HashSet<IPlayer> _players = new HashSet<IPlayer>();

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            await SetComponent(new StateComponent<StateHolder>());

            _tickEmitter = GrainFactory.GetPartitionGrain<ITickEmitter>(this);
            _collectableFinder = GrainFactory.GetPartitionGrain<ICollectableFinder>(this);
            _chunkTrackingHub = GrainFactory.GetPartitionGrain<IChunkTrackingHub>(this);
            _chunkColumn = GrainFactory.GetPartitionGrain<IChunkColumn>(this);
        }

        protected override async Task InitializeComponents()
        {
            _autoSave = new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute);
            await SetComponent(_autoSave);
        }

        public async Task Enter(IPlayer player)
        {
            bool active = _players.Count == 0;
            if (_players.Add(player))
            {
                var message = new DiscoveredByPlayer { Player = player };
                await Task.WhenAll(from e in State.DiscoveryEntities
                                   select e.Tell(message));

                if (active)
                    await World.ActivePartition(this);
            }
        }

        public async Task Leave(IPlayer player)
        {
            if (_players.Remove(player))
            {
                if (_players.Count == 0)
                {
                    await World.DeactivePartition(this);
                    DeactivateOnIdle();
                }
            }
        }

        public async Task OnGameTick(GameTickArgs e)
        {
            await Task.WhenAll(
                _tickEmitter.OnGameTick(e),
                _collectableFinder.OnGameTick(e),
                _chunkTrackingHub.OnGameTick(e),
                _chunkColumn.OnGameTick(e));
            await _autoSave.OnGameTick(this, e);
        }

        async Task IWorldPartition.SubscribeDiscovery(IEntity entity)
        {
            if (State.DiscoveryEntities.Add(entity))
            {
                MarkDirty();
                await entity.Tell(BroadcastDiscovered.Default);
            }
        }

        Task IWorldPartition.UnsubscribeDiscovery(IEntity entity)
        {
            if (State.DiscoveryEntities.Remove(entity))
                MarkDirty();
            return Task.CompletedTask;
        }

        private void MarkDirty()
        {
            ValueStorage.IsDirty = true;
        }

        internal class StateHolder
        {
            public HashSet<IEntity> DiscoveryEntities { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                DiscoveryEntities = new HashSet<IEntity>();
            }
        }
    }
}
