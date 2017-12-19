using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;
using Orleans.Streams;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class TickEmitterGrain : AddressByPartitionGrain, ITickEmitter
    {
        private ImmutableHashSet<IDependencyObject> _tickables = ImmutableHashSet<IDependencyObject>.Empty;

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
        }

        public async Task Subscribe(IDependencyObject observer)
        {
            bool active = _tickables.IsEmpty;
            _tickables = _tickables.Add(observer);

            if (active)
            {
                await GrainFactory.GetGrain<IGameSession>(World.GetPrimaryKeyString()).Subscribe(this);
            }
        }

        public async Task Unsubscribe(IDependencyObject observer)
        {
            _tickables = _tickables.Remove(observer);
            if (_tickables.IsEmpty)
                await GrainFactory.GetGrain<IGameSession>(World.GetPrimaryKeyString()).Unsubscribe(this);
        }

        public Task OnGameTick(GameTickArgs e)
        {
            var msg = new GameTick { Args = e };
            return Task.WhenAll(from t in _tickables select t.Tell(msg));
        }
    }
}
