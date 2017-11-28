﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MineCase.Engine.Serialization;
using Orleans.Streams;

namespace MineCase.Engine
{
    public partial class DependencyObject
    {
        private bool _isDestroyed = false;
        private readonly Queue<Func<Task>> _operationQueue = new Queue<Func<Task>>();

        protected ILogger Logger { get; private set; }

        public override async Task OnActivateAsync()
        {
            Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(GetType());
            InitializePreLoadComponent();
            await ReadStateAsync();
            InitializeComponents();
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

        protected virtual void InitializeComponents()
        {
        }

        protected virtual void InitializePreLoadComponent()
        {
        }

        public async Task ReadStateAsync()
        {
            var state = await DeserializeStateAsync();
            if (state == null || state.ValueStorage == null)
            {
                _valueStorage = new Data.DependencyValueStorage();
            }
            else
            {
                _valueStorage = (Data.DependencyValueStorage)state.ValueStorage;
            }

            _valueStorage.CurrentValueChanged += ValueStorage_CurrentValueChanged;
            await Tell(AfterReadState.Default);
        }

        public async Task WriteStateAsync()
        {
            try
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
                    ValueStorage.IsDirty = false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
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

        public IAsyncStream<T> GetStream<T>(string providerName, Guid streamId, string streamNamespace)
        {
            return GetStreamProvider(providerName).GetStream<T>(streamId, streamNamespace);
        }

        public new IDisposable RegisterTimer(Func<object, Task> callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            return base.RegisterTimer(callback, state, dueTime, period);
        }
    }
}
