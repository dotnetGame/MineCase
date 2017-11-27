using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class DiscoveryRegisterComponent : Component<EntityGrain>
    {
        public DiscoveryRegisterComponent(string name = "discoveryRegister")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += AddressByPartitionKeyChanged;
            AttachedObject.RegisterPropertyChangedHandler(IsEnabledComponent.IsEnabledProperty, OnIsEnabledChanged);
            AttachedObject.QueueOperation(TrySubscribe);
            return base.OnAttached();
        }

        protected override async Task OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= AddressByPartitionKeyChanged;
            await TryUnsubscribe();
        }

        private async Task AddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            if (!string.IsNullOrEmpty(e.oldKey))
                await GrainFactory.GetGrain<IWorldPartition>(e.oldKey).UnsubscribeDiscovery(AttachedObject);
            await TrySubscribe();
        }

        private Task OnIsEnabledChanged(object sender, PropertyChangedEventArgs<bool> e)
        {
            if (e.NewValue)
                return TrySubscribe();
            else
                return TryUnsubscribe();
        }

        private async Task TrySubscribe()
        {
            if (AttachedObject.GetValue(IsEnabledComponent.IsEnabledProperty))
            {
                var key = AttachedObject.GetAddressByPartitionKey();
                if (!string.IsNullOrEmpty(key))
                    await GrainFactory.GetGrain<IWorldPartition>(key).SubscribeDiscovery(AttachedObject);
            }
        }

        private async Task TryUnsubscribe()
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<IWorldPartition>(key).UnsubscribeDiscovery(AttachedObject);
        }
    }
}
