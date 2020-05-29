using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class HeldItemComponent : Component<PlayerGrain>, IHandle<SetHeldItemIndex>, IHandle<AskHeldItem, (int Index, Slot Slot)>, IHandle<SetHeldItem>
    {
        public static readonly DependencyProperty<int> HeldItemIndexProperty =
            DependencyProperty.Register("HeldItemIndex", typeof(HeldItemComponent), new PropertyMetadata<int>(0));

        public int HeldItemIndex => AttachedObject.GetValue(HeldItemIndexProperty);

        public HeldItemComponent(string name = "heldItem")
            : base(name)
        {
        }

        public async Task<(int Index, Slot Slot)> GetHeldItem()
        {
            var inventory = AttachedObject.GetComponent<InventoryComponent>().GetInventoryWindow();
            var index = await inventory.GetHotbarGlobalIndex(AttachedObject, HeldItemIndex);
            return (index, await inventory.GetSlot(AttachedObject, index));
        }

        public void SetHeldItemIndex(int index) =>
            AttachedObject.SetLocalValue(HeldItemIndexProperty, index);

        Task IHandle<SetHeldItemIndex>.Handle(SetHeldItemIndex message)
        {
            SetHeldItemIndex(message.Index);
            return Task.CompletedTask;
        }

        Task<(int Index, Slot Slot)> IHandle<AskHeldItem, (int Index, Slot Slot)>.Handle(AskHeldItem message) =>
            GetHeldItem();

        async Task IHandle<SetHeldItem>.Handle(SetHeldItem message)
        {
            var inventory = AttachedObject.GetComponent<InventoryComponent>().GetInventoryWindow();
            var index = await inventory.GetHotbarGlobalIndex(AttachedObject, HeldItemIndex);
            await inventory.SetSlot(AttachedObject, index, message.Slot);
        }
    }
}
