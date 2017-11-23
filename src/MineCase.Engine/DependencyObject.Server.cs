using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Serialization;

namespace MineCase.Engine
{
    public partial class DependencyObject
    {
        private bool _isDestroyed = false;
        private readonly Queue<Func<Task>> _operationQueue = new Queue<Func<Task>>();

        public override async Task OnActivateAsync()
        {
            await InitializePreLoadComponent();
            await ReadStateAsync();
            await InitializeComponents();
            await ClearOperationQueue();
        }

        public override async Task OnDeactivateAsync()
        {
            await WriteStateAsync();
            await base.OnDeactivateAsync();
        }

        public void Destroy()
        {
            _isDestroyed = true;
            DeactivateOnIdle();
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
            bool needSave = false;
            var state = await DeserializeStateAsync();
            if (state == null || state.ValueStorage == null)
            {
                _valueStorage = new Data.DependencyValueStorage();
                needSave = true;
            }
            else
            {
                _valueStorage = (Data.DependencyValueStorage)state.ValueStorage;
            }

            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
            await Tell(AfterReadState.Default);

            if (needSave)
                await WriteStateAsync();
        }

        public async Task WriteStateAsync()
        {
            if (_isDestroyed)
            {
                await ClearStateAsync();
            }
            else
            {
                await Tell(BeforeWriteState.Default);
                var state = new DependencyObjectState
                {
                    GrainKeyString = GrainReference.ToKeyString(),
                    ValueStorage = _valueStorage
                };

                await SerializeStateAsync(state);
            }
        }

        protected virtual Task<DependencyObjectState> DeserializeStateAsync()
        {
            return Task.FromResult<DependencyObjectState>(null);
        }

        protected virtual Task SerializeStateAsync(DependencyObjectState state)
        {
            return Task.CompletedTask;
        }

        protected virtual Task ClearStateAsync()
        {
            return Task.CompletedTask;
        }

        public void QueueOperation(Func<Task> operation)
        {
            _operationQueue.Enqueue(operation);
        }

        public async Task ClearOperationQueue()
        {
            while (_operationQueue.Count != 0)
            {
                await _operationQueue.Dequeue()();
            }
        }
    }
}
