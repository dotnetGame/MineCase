using System;
using System.Collections.Generic;
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

namespace MineCase.Server.World
{
    [PersistTableName("tickEmitter")]
    [Reentrant]
    internal class TickEmitterGrain : PersistableDependencyObject, ITickEmitter
    {
        private AutoSaveStateComponent _autoSave;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override async Task InitializePreLoadComponent()
        {
            await SetComponent(new StateComponent<StateHolder>());
        }

        protected override async Task InitializeComponents()
        {
            _autoSave = new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute);
            await SetComponent(_autoSave);
        }

        public async Task OnGameTick(GameTickArgs e)
        {
            var message = new GameTick { Args = e };
            await Task.WhenAll(from en in State.Subscription select en.Tell(message));
            await _autoSave.OnGameTick(this, e);
        }

        public Task Subscribe(IDependencyObject observer)
        {
            if (State.Subscription.Add(observer))
                MarkDirty();
            return Task.CompletedTask;
        }

        public Task Unsubscribe(IDependencyObject observer)
        {
            if (State.Subscription.Remove(observer))
                MarkDirty();
            return Task.CompletedTask;
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
