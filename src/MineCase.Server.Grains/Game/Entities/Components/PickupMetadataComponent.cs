using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.EntityMetadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PickupMetadataComponent : Component<EntityGrain>, IHandle<SetSlot>
    {
        public static readonly DependencyProperty<Pickup> PickupMetadataProperty =
            DependencyProperty.Register<Pickup>("PickupMetadata", typeof(PickupMetadataComponent));

        public Pickup PickupMetadata => AttachedObject.GetValue(PickupMetadataProperty);

        public PickupMetadataComponent(string name = "pickupMetadata")
            : base(name)
        {
        }

        protected override async Task OnAttached()
        {
            await base.OnAttached();
            await AttachedObject.SetLocalValue(PickupMetadataProperty, new Pickup());
        }

        async Task IHandle<SetSlot>.Handle(SetSlot message)
        {
            PickupMetadata.Item = message.Slot;
            await AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .EntityMetadata(AttachedObject.EntityId, PickupMetadata);
        }
    }
}
