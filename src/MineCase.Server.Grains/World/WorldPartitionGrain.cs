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

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            await SetComponent(new StateComponent<StateHolder>());

            _tickEmitter = GrainFactory.GetPartitionGrain<ITickEmitter>(this);
        }

        public async Task Enter(IPlayer player)
        {
            var players = State.Players;
            var active = players.Count == 0;
            players.Add(player);

            var message = new DiscoveredByPlayer { Player = player };
            await Task.WhenAll(from e in State.DiscoveryEntities
                               select e.Tell(message));

            if (active)
                await World.ActivePartition(this);
        }

        public async Task Leave(IPlayer player)
        {
            var playrs = State.Players;
            playrs.Remove(player);
            if (playrs.Count == 0)
            {
                await World.DeactivePartition(this);
                DeactivateOnIdle();
            }
        }

        public Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            _tickEmitter.InvokeOneWay(e => e.OnGameTick(deltaTime, worldAge));
            return Task.CompletedTask;
        }

        async Task IWorldPartition.SubscribeDiscovery(IEntity entity)
        {
            State.DiscoveryEntities.Add(entity);
            await entity.Tell(BroadcastDiscovered.Default);
        }

        Task IWorldPartition.UnsubscribeDiscovery(IEntity entity)
        {
            State.DiscoveryEntities.Remove(entity);
            return Task.CompletedTask;
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
