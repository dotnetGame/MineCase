using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using Orleans;

namespace MineCase.Server.World
{
    internal class WorldPartitionGrain : AddressByPartitionGrain, IWorldPartition
    {
        private ITickEmitter _tickEmitter;
        private HashSet<IPlayer> _players;
        private HashSet<IEntity> _discoveryEntities;

        public override Task OnActivateAsync()
        {
            _tickEmitter = GrainFactory.GetPartitionGrain<ITickEmitter>(this);
            _players = new HashSet<IPlayer>();
            _discoveryEntities = new HashSet<IEntity>();
            return base.OnActivateAsync();
        }

        public async Task Enter(IPlayer player)
        {
            var active = _players.Count == 0;
            _players.Add(player);

            var message = new DiscoveredByPlayer { Player = player };
            await Task.WhenAll(from e in _discoveryEntities
                               select e.Tell(message));

            if (active)
                await World.ActivePartition(this);
        }

        public async Task Leave(IPlayer player)
        {
            _players.Remove(player);
            if (_players.Count == 0)
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

        Task IWorldPartition.SubscribeDiscovery(IEntity entity)
        {
            _discoveryEntities.Add(entity);
            return Task.CompletedTask;
        }

        Task IWorldPartition.UnsubscribeDiscovery(IEntity entity)
        {
            _discoveryEntities.Remove(entity);
            return Task.CompletedTask;
        }
    }
}
