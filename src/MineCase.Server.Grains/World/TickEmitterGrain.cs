using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
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
    internal class TickEmitterGrain : Grain, ITickEmitter
    {
        private ImmutableHashSet<IDependencyObject> _tickables = ImmutableHashSet<IDependencyObject>.Empty;

        public Task OnGameTick(GameTickArgs e)
        {
            var msg = new GameTick { Args = e };
            return Task.WhenAll(from t in _tickables select t.Tell(msg));
        }

        public Task Subscribe(IDependencyObject observer)
        {
            _tickables = _tickables.Add(observer);
            return Task.CompletedTask;
        }

        public Task Unsubscribe(IDependencyObject observer)
        {
            _tickables = _tickables.Remove(observer);
            return Task.CompletedTask;
        }
    }
}
