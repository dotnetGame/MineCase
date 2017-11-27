using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class DiscoveryRegisterComponent : Component<EntityGrain>, IHandle<Disable>
    {
        public DiscoveryRegisterComponent(string name = "discoveryRegister")
            : base(name)
        {
        }

        protected override async Task OnAttached()
        {
            await base.OnAttached();
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += AddressByPartitionKeyChanged;
        }

        private async Task AddressByPartitionKeyChanged(object sender, (string oldKey, string newKey) e)
        {
            if (!string.IsNullOrEmpty(e.oldKey))
                await GrainFactory.GetGrain<IWorldPartition>(e.oldKey).UnsubscribeDiscovery(AttachedObject);
            if (!string.IsNullOrEmpty(e.newKey))
                await GrainFactory.GetGrain<IWorldPartition>(e.newKey).SubscribeDiscovery(AttachedObject);
        }

        async Task IHandle<Disable>.Handle(Disable message)
        {
            var key = AttachedObject.GetAddressByPartitionKey();
            if (!string.IsNullOrEmpty(key))
                await GrainFactory.GetGrain<IWorldPartition>(key).UnsubscribeDiscovery(AttachedObject);
        }
    }
}
