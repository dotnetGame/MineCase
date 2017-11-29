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

        protected override void OnAttached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged += ActiveWorldPartitionComponent_KeyChanged;
        }

        protected override void OnDetached()
        {
            AttachedObject.GetComponent<AddressByPartitionKeyComponent>()
                .KeyChanged -= ActiveWorldPartitionComponent_KeyChanged;
        }

        private void ActiveWorldPartitionComponent_KeyChanged(object sender, (string oldKey, string newKey) e)
        {
            AttachedObject.QueueOperation(async () =>
            {
                if (!string.IsNullOrEmpty(e.oldKey))
                    await GrainFactory.GetGrain<IWorldPartition>(e.oldKey).Leave(AttachedObject);
                if (!string.IsNullOrEmpty(e.newKey))
                    await GrainFactory.GetGrain<IWorldPartition>(e.newKey).Enter(AttachedObject);
            });
        }
    }
}
