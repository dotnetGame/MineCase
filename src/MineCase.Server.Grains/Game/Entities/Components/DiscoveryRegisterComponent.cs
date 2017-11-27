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

        protected override void OnAttached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += AddressByPartitionKeyChanged;
            AttachedObject.RegisterPropertyChangedHandler(IsEnabledComponent.IsEnabledProperty, OnIsEnabledChanged);
            AttachedObject.QueueOperation(TrySubscribe);
        }

        protected override void OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= AddressByPartitionKeyChanged;
            AttachedObject.QueueOperation(TryUnsubscribe);
        }

        private void AddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            AttachedObject.QueueOperation(async () =>
            {
                if (!string.IsNullOrEmpty(e.oldKey))
                    await GrainFactory.GetGrain<IWorldPartition>(e.oldKey).UnsubscribeDiscovery(AttachedObject);
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
