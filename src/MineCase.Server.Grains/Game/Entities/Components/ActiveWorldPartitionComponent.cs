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

        private void ActiveWorldPartitionComponent_KeyChanged(object sender, (string OldKey, string NewKey) e)
        {
            AttachedObject.QueueOperation(async () =>
            {
                if (!string.IsNullOrEmpty(e.OldKey))
                    await GrainFactory.GetGrain<IWorldPartition>(e.OldKey).Leave(AttachedObject);
                if (!string.IsNullOrEmpty(e.NewKey))
                    await GrainFactory.GetGrain<IWorldPartition>(e.NewKey).Enter(AttachedObject);
            });
        }
    }
}
