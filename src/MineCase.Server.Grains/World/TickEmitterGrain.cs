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
using Orleans.Streams;

namespace MineCase.Server.World
{
    [PersistTableName("tickEmitter")]
    [Reentrant]
    internal class TickEmitterGrain : PersistableDependencyObject, ITickEmitter
    {
        private AutoSaveStateComponent _autoSave;
        private IAsyncStream<GameTickArgs> _tickStream;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        protected override void InitializePreLoadComponent()
        {
            SetComponent(new StateComponent<StateHolder>());
        }

        protected override void InitializeComponents()
        {
            _autoSave = new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute);
            SetComponent(_autoSave);
        }

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _tickStream = GetStreamProvider(StreamProviders.TransientProvider).GetStream<GameTickArgs>(State.SubscriptionStreamId, StreamProviders.Namespaces.TickEmitter);
        }

        public async Task OnGameTick(GameTickArgs e)
        {
            await _tickStream.OnNextAsync(e);
            await _autoSave.OnGameTick(this, e);
        }

        private void MarkDirty()
        {
            ValueStorage.IsDirty = true;
        }

        public Task<Guid> GetSubscriptionStreamId() => Task.FromResult(State.SubscriptionStreamId);

        internal class StateHolder
        {
            public Guid SubscriptionStreamId { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                SubscriptionStreamId = Guid.NewGuid();
            }
        }
    }
}
