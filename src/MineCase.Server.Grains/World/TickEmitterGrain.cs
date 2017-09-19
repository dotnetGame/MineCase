using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World
{
    internal class TickEmitterGrain : Grain, ITickEmitter
    {
        private HashSet<IDependencyObject> _subscription;

        public override Task OnActivateAsync()
        {
            _subscription = new HashSet<IDependencyObject>();
            return base.OnActivateAsync();
        }

        public Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            var message = new GameTick { DeltaTime = deltaTime, WorldAge = worldAge };
            foreach (var entity in _subscription)
                entity.InvokeOneWay(e => e.Tell(message));
            return Task.CompletedTask;
        }

        public Task Subscribe(IDependencyObject observer)
        {
            _subscription.Add(observer);
            return Task.CompletedTask;
        }

        public Task Unsubscribe(IDependencyObject observer)
        {
            _subscription.Remove(observer);
            return Task.CompletedTask;
        }
    }
}
