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
        private HashSet<IPlayer> _players = new HashSet<IPlayer>();
        private ITickEmitter _tickEmitter;
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

        public Task Enter(IPlayer player)
        {
            bool active = _players.Count == 0;
            if (_players.Add(player))
            {
                var message = new DiscoveredByPlayer { Player = player };
                foreach (var entity in State.DiscoveryEntities)
                {
                    if (entity.Equals(player)) continue;
                    entity.InvokeOneWay(g => g.Tell(message));
                }

                if (active && State.IsTickEmitterActive)
                    return _fixedUpdate.Start(World);
            }

            return Task.CompletedTask;
        }

        public Task Leave(IPlayer player)
        {
            if (_players.Remove(player))
            {
                if (_players.Count == 0)
                {
                    _fixedUpdate.Stop();
                    DeactivateOnIdle();
                }
            }

            return Task.CompletedTask;
        }

        Task IWorldPartition.SubscribeDiscovery(IEntity entity)
        {
            if (State.DiscoveryEntities.Add(entity))
            {
                MarkDirty();
                entity.InvokeOneWay(e => e.Tell(BroadcastDiscovered.Default));
            }

            return Task.CompletedTask;
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

        public Task SubscribeTickEmitter(ITickEmitter tickEmitter)
        {
            if (!State.IsTickEmitterActive)
            {
                State.IsTickEmitterActive = true;
                MarkDirty();
                if (_players.Count != 0)
                    return _fixedUpdate.Start(World);
            }

            return Task.CompletedTask;
        }

        public Task UnsubscribeTickEmitter(ITickEmitter tickEmitter)
        {
            if (State.IsTickEmitterActive)
            {
                State.IsTickEmitterActive = false;
                MarkDirty();
                _fixedUpdate.Stop();
            }

            return Task.CompletedTask;
        }

        internal class StateHolder
        {
            public HashSet<IEntity> DiscoveryEntities { get; set; }

            public bool IsTickEmitterActive { get; set; }

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
