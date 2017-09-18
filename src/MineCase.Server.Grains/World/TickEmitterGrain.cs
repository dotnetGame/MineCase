using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World
{
    internal class TickEmitterGrain : Grain, ITickEmitter
    {
        private ObserverSubscriptionManager<ITickObserver> _subscriptionManager;

        public override Task OnActivateAsync()
        {
            _subscriptionManager = new ObserverSubscriptionManager<ITickObserver>();
            return base.OnActivateAsync();
        }

        public Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            _subscriptionManager.Notify(o => o.OnGameTick(deltaTime, worldAge));
            return Task.CompletedTask;
        }

        public Task Subscribe(ITickObserver observer)
        {
            _subscriptionManager.Subscribe(observer);
            return Task.CompletedTask;
        }

        public Task Unsubscribe(ITickObserver observer)
        {
            _subscriptionManager.Unsubscribe(observer);
            return Task.CompletedTask;
        }
    }
}
