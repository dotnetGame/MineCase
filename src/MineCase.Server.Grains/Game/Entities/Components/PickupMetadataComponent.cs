using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.EntityMetadata;
using MineCase.Server.Game.Items;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PickupMetadataComponent : Component<EntityGrain>, IHandle<SetSlot>, IHandle<CollectBy>, IHandle<AskCollectionResult, Slot>
    {
        public static readonly DependencyProperty<Pickup> PickupMetadataProperty =
            DependencyProperty.Register<Pickup>("PickupMetadata", typeof(PickupMetadataComponent));

        public Pickup PickupMetadata => AttachedObject.GetValue(PickupMetadataProperty);

        public PickupMetadataComponent(string name = "pickupMetadata")
            : base(name)
        {
        }

        protected override void OnAttached()
        {
            AttachedObject.SetLocalValue(PickupMetadataProperty, new Pickup());
        }

        async Task IHandle<SetSlot>.Handle(SetSlot message)
        {
            await UpdateItem(message.Slot);
        }

        async Task IHandle<CollectBy>.Handle(CollectBy message)
        {
            var result = await message.Entity.Ask(new AskCollectionResult
            {
                Source = AttachedObject,
                Slot = PickupMetadata.Item
            });
            result.MakeEmptyIfZero();
            if (result.IsEmpty)
            {
                await AttachedObject.Tell(DestroyEntity.Default);
            }
            else if (PickupMetadata.Item.ItemCount != result.ItemCount)
            {
                await UpdateItem(result);
            }
        }

        private async Task UpdateItem(Slot slot)
        {
            PickupMetadata.Item = slot;
            await AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .EntityMetadata(AttachedObject.EntityId, PickupMetadata);
        }

        async Task<Slot> IHandle<AskCollectionResult, Slot>.Handle(AskCollectionResult message)
        {
            var slot = message.Slot;
            if (slot.CanStack(PickupMetadata.Item))
            {
                var maxStack = ItemHandler.Create((uint)slot.BlockId).MaxStackCount;
                var toStack = Math.Min(maxStack, slot.ItemCount + PickupMetadata.Item.ItemCount) - PickupMetadata.Item.ItemCount;
                if (toStack > 0)
                {
                    await UpdateItem(PickupMetadata.Item.AddItemCount(toStack));
                    slot.ItemCount -= (byte)toStack;
                    slot.MakeEmptyIfZero();
                }
            }

            return slot;
        }
    }
}
