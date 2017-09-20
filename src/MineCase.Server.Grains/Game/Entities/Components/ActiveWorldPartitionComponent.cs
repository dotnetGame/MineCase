using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ActiveWorldPartitionComponent : Component<PlayerGrain>
    {
        public ActiveWorldPartitionComponent(string name = "activeWorldPartition")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += ActiveWorldPartitionComponent_KeyChanged;
            return base.OnAttached();
        }

        protected override Task OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= ActiveWorldPartitionComponent_KeyChanged;
            return base.OnDetached();
        }

        private async Task ActiveWorldPartitionComponent_KeyChanged(object sender, (string oldKey, string newKey) e)
        {
            if (!string.IsNullOrEmpty(e.oldKey))
                await GrainFactory.GetGrain<IWorldPartition>(e.oldKey).Leave(AttachedObject);
            if (!string.IsNullOrEmpty(e.newKey))
                await GrainFactory.GetGrain<IWorldPartition>(e.newKey).Enter(AttachedObject);
        }
    }
}
