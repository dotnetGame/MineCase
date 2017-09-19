﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class HeldItemComponent : Component<PlayerGrain>, IHandle<SetHeldItemIndex>, IHandle<AskHeldItem, (int index, Slot slot)>
    {
        public static readonly DependencyProperty<int> HeldItemIndexProperty =
            DependencyProperty.Register("HeldItemIndex", typeof(HeldItemComponent), new PropertyMetadata<int>(0));

        public int HeldItemIndex => AttachedObject.GetValue(HeldItemIndexProperty);

        public HeldItemComponent(string name = "heldItem")
            : base(name)
        {
        }

        public async Task<(int index, Slot slot)> GetHeldItem()
        {
            var inventory = AttachedObject.GetComponent<InventoryComponent>().GetInventoryWindow();
            var index = await inventory.GetHotbarGlobalIndex(AttachedObject, HeldItemIndex);
            return (index, await inventory.GetSlot(AttachedObject, index));
        }

        public Task SetHeldItemIndex(int index) =>
            AttachedObject.SetLocalValue(HeldItemIndexProperty, index);

        Task IHandle<SetHeldItemIndex>.Handle(SetHeldItemIndex message) =>
            SetHeldItemIndex(message.Index);

        Task<(int index, Slot slot)> IHandle<AskHeldItem, (int index, Slot slot)>.Handle(AskHeldItem message) =>
            GetHeldItem();
    }
}
