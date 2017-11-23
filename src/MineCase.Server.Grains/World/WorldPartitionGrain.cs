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
            var players = State.Players;
            var active = players.Count == 0;
            if (players.Add(player))
            {
                MarkDirty();
                var message = new DiscoveredByPlayer { Player = player };
                await Task.WhenAll(from e in State.DiscoveryEntities
                                   select e.Tell(message));

                if (active)
                    await World.ActivePartition(this);
            }
        }

        public async Task Leave(IPlayer player)
        {
            var players = State.Players;
            if (players.Remove(player))
            {
                MarkDirty();
                if (players.Count == 0)
                {
                    await World.DeactivePartition(this);
                    DeactivateOnIdle();
                }
            }
        }

        public async Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            await Task.WhenAll(
                _tickEmitter.OnGameTick(deltaTime, worldAge),
                _collectableFinder.OnGameTick(deltaTime, worldAge),
                _chunkTrackingHub.OnGameTick(deltaTime, worldAge),
                _chunkColumn.OnGameTick(deltaTime, worldAge));
            await _autoSave.OnGameTick(this, (deltaTime, worldAge));
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
            _autoSave.IsDirty = true;
        }

        internal class StateHolder
        {
            public HashSet<IPlayer> Players { get; set; }

            public HashSet<IEntity> DiscoveryEntities { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                Players = new HashSet<IPlayer>();
                DiscoveryEntities = new HashSet<IEntity>();
            }
        }
    }
}
