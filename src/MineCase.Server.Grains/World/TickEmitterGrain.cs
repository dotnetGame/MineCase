using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities;
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [PersistTableName("tickEmitter")]
    [Reentrant]
    internal class TickEmitterGrain : PersistableDependencyObject, ITickEmitter
    {
        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            await SetComponent(new StateComponent<StateHolder>());
        }

        public Task OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            var message = new GameTick { DeltaTime = deltaTime, WorldAge = worldAge };
            foreach (var entity in State.Subscription)
                entity.InvokeOneWay(e => e.Tell(message));
            return Task.CompletedTask;
        }

        public Task Subscribe(IDependencyObject observer)
        {
            State.Subscription.Add(observer);
            return Task.CompletedTask;
        }

        public Task Unsubscribe(IDependencyObject observer)
        {
            State.Subscription.Remove(observer);
            return Task.CompletedTask;
        }

        internal class StateHolder
        {
            public HashSet<IDependencyObject> Subscription { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                Subscription = new HashSet<IDependencyObject>();
            }
        }
    }
}
