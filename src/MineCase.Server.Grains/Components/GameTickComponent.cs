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
    internal class GameTickComponent : Component, ITickObserver
    {
        public event AsyncEventHandler<(TimeSpan deltaTime, long worldAge)> Tick;

        private ITickObserver _tickObserver;

        public GameTickComponent(string name = "gameTick")
            : base(name)
        {
        }

        protected override async Task OnAttached()
        {
            _tickObserver = await GrainFactory.CreateObjectReference<ITickObserver>(this);

            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += OnAddressByPartitionKeyChanged;
        }

        protected override async Task OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= OnAddressByPartitionKeyChanged;
            await GrainFactory.DeleteObjectReference<ITickObserver>(this);
            _tickObserver = null;
        }

        private async Task OnAddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            if (!string.IsNullOrEmpty(e.oldKey))
                await GrainFactory.GetGrain<ITickEmitter>(e.oldKey).Unsubscribe(_tickObserver);
            if (!string.IsNullOrEmpty(e.newKey))
                await GrainFactory.GetGrain<ITickEmitter>(e.newKey).Subscribe(_tickObserver);
        }

        public void OnGameTick(TimeSpan deltaTime, long worldAge)
        {
            Tick.InvokeSerial(this, (deltaTime, worldAge)).Ignore();
        }
    }
}
