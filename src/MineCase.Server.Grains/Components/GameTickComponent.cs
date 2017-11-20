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
    internal class GameTickComponent : Component, IHandle<GameTick>, IHandle<Disable>
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
            return base.OnAttached();
        }

        protected override Task OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= OnAddressByPartitionKeyChanged;
            return base.OnDetached();
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

        async Task IHandle<Disable>.Handle(Disable message)
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<ITickEmitter>(key).Unsubscribe(AttachedObject);
        }
    }
}
