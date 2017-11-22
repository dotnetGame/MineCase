using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Serialization;

namespace MineCase.Engine
{
    public partial class DependencyObject
    {
        public override async Task OnActivateAsync()
        {
            await InitializePreLoadComponent();
            await ReadStateAsync();
            await InitializeComponents();
        }

        public override async Task OnDeactivateAsync()
        {
            await WriteStateAsync();
            await base.OnDeactivateAsync();
        }

        protected virtual Task InitializeComponents()
        {
            return Task.CompletedTask;
        }

        protected virtual Task InitializePreLoadComponent()
        {
            return Task.CompletedTask;
        }

        public async Task ReadStateAsync()
        {
            await Tell(BeforeReadState.Default);
            var state = await DeserializeStateAsync();
            _valueStorage = (Data.DependencyValueStorage)state?.ValueStorage ?? new Data.DependencyValueStorage();
            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
            await Tell(AfterReadState.Default);
        }

        public async Task WriteStateAsync()
        {
            var state = new DependencyObjectState
            {
                GrainKeyString = GrainReference.ToKeyString(),
                ValueStorage = _valueStorage
            };

            await SerializeStateAsync(state);
        }

        protected virtual Task<DependencyObjectState> DeserializeStateAsync()
        {
            return Task.FromResult<DependencyObjectState>(null);
        }

        protected virtual Task SerializeStateAsync(DependencyObjectState state)
        {
            return Task.CompletedTask;
        }
    }
}
