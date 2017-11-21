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
            await LoadStateAsync();
            await InitializeComponents();
        }

        public override async Task OnDeactivateAsync()
        {
            await SaveStateAsync();
            await base.OnDeactivateAsync();
        }

        protected virtual Task InitializeComponents()
        {
            return Task.CompletedTask;
        }

        public async Task LoadStateAsync()
        {
            var state = await DeserializeStateAsync();
            _valueStorage = (Data.DependencyValueStorage)state?.ValueStorage ?? new Data.DependencyValueStorage();
            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
        }

        public Task SaveStateAsync()
        {
            var state = new DependencyObjectState
            {
                GrainKeyString = GrainReference.ToKeyString(),
                ValueStorage = _valueStorage
            };

            return SerializeStateAsync(state);
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
