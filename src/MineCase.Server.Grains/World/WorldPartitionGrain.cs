using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private HashSet<IPlayer> _players = new HashSet<IPlayer>();
        private IDisposable _tickTimer;
        private Stopwatch _stopwatch;
        private long _worldAge;
        private long _actualAge;
        private static readonly long _updateTick = TimeSpan.FromMilliseconds(50).Ticks;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override void InitializePreLoadComponent()
        {
            SetComponent(new StateComponent<StateHolder>());

            _tickEmitter = GrainFactory.GetPartitionGrain<ITickEmitter>(this);
        }

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
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
                    _worldAge = await World.GetAge();
                    _actualAge = 0;
                    _stopwatch = new Stopwatch();
                    _stopwatch.Start();
                    _tickTimer = RegisterTimer(OnTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(5));
                }
            }
        }

        private async Task OnTick(object arg)
        {
            var expectedAge = _stopwatch.ElapsedTicks / _updateTick;
            var e = new GameTickArgs { DeltaTime = TimeSpan.FromMilliseconds(50) };
            var updateTimes = expectedAge - _actualAge;
            for (int i = 0; i < updateTimes; i++)
            {
                e.WorldAge = _worldAge;
                e.TimeOfDay = _worldAge % 24000;
                await _tickEmitter.OnGameTick(e);
                _worldAge++;
                _actualAge++;
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
