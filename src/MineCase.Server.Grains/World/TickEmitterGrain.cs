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
    [PersistTableName("tickEmitter")]
    internal class TickEmitterGrain : AddressByPartitionGrain, ITickEmitter
    {
        private IWorldPartition _worldPartition;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override void InitializePreLoadComponent()
        {
            SetComponent(new StateComponent<StateHolder>());
            _worldPartition = GrainFactory.GetPartitionGrain<IWorldPartition>(this);
        }

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
        }

        public Task Subscribe(IDependencyObject observer)
        {
            bool active = State.Subscription.Count == 0;
            if (State.Subscription.Add(observer))
            {
                MarkDirty();
                if (active)
                    return _worldPartition.SubscribeTickEmitter(this);
            }

            return Task.CompletedTask;
        }

        public Task Unsubscribe(IDependencyObject observer)
        {
            if (State.Subscription.Remove(observer))
            {
                MarkDirty();
                if (State.Subscription.Count == 0)
                    return _worldPartition.UnsubscribeTickEmitter(this);
            }

            return Task.CompletedTask;
        }

        public Task OnGameTick(GameTickArgs e)
        {
            var msg = new GameTick { Args = e };
            return Task.WhenAll(from t in State.Subscription select t.Tell(msg));
        }

        private void MarkDirty()
        {
            ValueStorage.IsDirty = true;
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
