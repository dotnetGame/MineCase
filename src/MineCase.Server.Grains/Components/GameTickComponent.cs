using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Components
{
    internal class GameTickComponent : Component, IHandle<GameTick>
    {
        public event AsyncEventHandler<(TimeSpan deltaTime, long worldAge)> Tick;

        public GameTickComponent(string name = "gameTick")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += OnAddressByPartitionKeyChanged;
            AttachedObject.RegisterPropertyChangedHandler(IsEnabledComponent.IsEnabledProperty, OnIsEnabledChanged);
            AttachedObject.QueueOperation(TrySubscribe);
            return base.OnAttached();
        }

        protected override async Task OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= OnAddressByPartitionKeyChanged;
            await TryUnsubscribe();
        }

        private async Task OnAddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            if (!string.IsNullOrEmpty(e.oldKey))
                await GrainFactory.GetGrain<ITickEmitter>(e.oldKey).Unsubscribe(AttachedObject);
            await TrySubscribe();
        }

        private Task OnIsEnabledChanged(object sender, PropertyChangedEventArgs<bool> e)
        {
            if (e.NewValue)
                return TrySubscribe();
            else
                return TryUnsubscribe();
        }

        public void OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            Tick.InvokeSerial(this, (deltaTime, worldAge)).Ignore();
        }

        Task IHandle<GameTick>.Handle(GameTick message)
        {
            return Tick.InvokeSerial(this, (message.DeltaTime, message.WorldAge));
        }

        private async Task TrySubscribe()
        {
            if (AttachedObject.GetValue(IsEnabledComponent.IsEnabledProperty))
            {
                var key = AttachedObject.GetAddressByPartitionKey();
                if (!string.IsNullOrEmpty(key))
                    await GrainFactory.GetGrain<ITickEmitter>(key).Subscribe(AttachedObject);
            }
        }

        private async Task TryUnsubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<ITickEmitter>(key).Unsubscribe(AttachedObject);
        }
    }
}
