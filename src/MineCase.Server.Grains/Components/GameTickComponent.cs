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
    internal class GameTickComponent : Component, IHandle<GameTick>, IHandle<Disable>, IHandle<Enable>
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
            if (!string.IsNullOrEmpty(e.newKey))
                await GrainFactory.GetGrain<ITickEmitter>(e.newKey).Subscribe(AttachedObject);
        }

        public void OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            Tick.InvokeSerial(this, (deltaTime, worldAge)).Ignore();
        }

        Task IHandle<GameTick>.Handle(GameTick message)
        {
            return Tick.InvokeSerial(this, (message.DeltaTime, message.WorldAge));
        }

        Task IHandle<Enable>.Handle(Enable message)
        {
            return TrySubscribe();
        }

        Task IHandle<Disable>.Handle(Disable message)
        {
            return TryUnsubscribe();
        }

        private async Task TrySubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<ITickEmitter>(key).Subscribe(AttachedObject);
        }

        private async Task TryUnsubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<ITickEmitter>(key).Unsubscribe(AttachedObject);
        }
    }
}
