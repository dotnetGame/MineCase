using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Components
{
    internal class GameTickComponent : Component, IHandle<GameTick>
    {
        public event AsyncEventHandler<GameTickArgs> Tick;

        public GameTickComponent(string name = "gameTick")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += OnAddressByPartitionKeyChanged;
            AttachedObject.RegisterPropertyChangedHandler(IsEnabledComponent.IsEnabledProperty, OnIsEnabledChanged);
            AttachedObject.QueueOperation(TrySubscribe);
        }

        protected override void OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= OnAddressByPartitionKeyChanged;
            AttachedObject.QueueOperation(TryUnsubscribe);
        }

        private void OnAddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            AttachedObject.QueueOperation(async () =>
            {
                /*
                if (!string.IsNullOrEmpty(e.oldKey))
                    await GrainFactory.GetGrain<ITickEmitter>(e.oldKey).Unsubscribe(AttachedObject);*/
                await TrySubscribe();
            });
        }

        private void OnIsEnabledChanged(object sender, PropertyChangedEventArgs<bool> e)
        {
            if (e.NewValue)
                AttachedObject.QueueOperation(TrySubscribe);
            else
                AttachedObject.QueueOperation(TryUnsubscribe);
        }

        public Task OnGameTick(GameTickArgs e)
        {
            return Tick.InvokeSerial(this, e);
        }

        Task IHandle<GameTick>.Handle(GameTick message)
        {
            return OnGameTick(message.Args);
        }

        private async Task TrySubscribe()
        {
            if (AttachedObject.GetValue(IsEnabledComponent.IsEnabledProperty))
            {
                var key = AttachedObject.GetAddressByPartitionKey();
                if (!string.IsNullOrEmpty(key))
                    await Task.CompletedTask; // GrainFactory.GetGrain<ITickEmitter>(key).Subscribe(AttachedObject);
            }
        }

        private async Task TryUnsubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await Task.CompletedTask; // await GrainFactory.GetGrain<ITickEmitter>(key).Unsubscribe(AttachedObject);
        }
    }
}
