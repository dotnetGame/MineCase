using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Components;
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
        private HashSet<IPlayer> _players = new HashSet<IPlayer>();
        private FixedUpdateComponent _fixedUpdate;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override void InitializePreLoadComponent()
        {
            SetComponent(new StateComponent<StateHolder>());

            _tickEmitter = GrainFactory.GetPartitionGrain<ITickEmitter>(this);
        }

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));

            _fixedUpdate = new FixedUpdateComponent();
            _fixedUpdate.Tick += OnFixedUpdate;
            SetComponent(_fixedUpdate);
        }

        private Task OnFixedUpdate(object sender, GameTickArgs e)
        {
            return _tickEmitter.OnGameTick(e);
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
                {
                    await World.ActivePartition(this);
                    await _fixedUpdate.Start(World);
                }
            }
        }

        public async Task Leave(IPlayer player)
        {
            if (_players.Remove(player))
            {
                if (_players.Count == 0)
                {
                    await World.DeactivePartition(this);
                    _fixedUpdate.Stop();
                    DeactivateOnIdle();
                }
            }
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

        public Task OnGameTick(GameTickArgs e)
        {
            return _tickEmitter.OnGameTick(e);
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
