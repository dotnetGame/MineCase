using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World
{
    internal class WorldPartitionGrain : AddressByPartitionGrain, IWorldPartition
    {
        private ITickEmitter _tickEmitter;
        private HashSet<IPlayer> _players;

        public override Task OnActivateAsync()
        {
            _tickEmitter = GrainFactory.GetPartitionGrain<ITickEmitter>(this);
            _players = new HashSet<IPlayer>();
            return base.OnActivateAsync();
        }

        public async Task Enter(IPlayer player)
        {
            var active = _players.Count == 0;
            _players.Add(player);

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

        public void OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            _tickEmitter.InvokeOneWay(e => e.OnGameTick(deltaTime, worldAge));
        }
    }
}
