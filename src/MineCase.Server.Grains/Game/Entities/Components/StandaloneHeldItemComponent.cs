using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class StandaloneHeldItemComponent : Component<EntityGrain>, IHandle<SetHeldItemIndex>, IHandle<AskHeldItem, (int Index, Slot Slot)>, IHandle<SetHeldItem>
    {
        public static readonly DependencyProperty<Slot> HeldItemProperty =
            DependencyProperty.Register("HeldItem", typeof(StandaloneHeldItemComponent), new PropertyMetadata<Slot>(Slot.Empty));

        public Slot HeldItem => AttachedObject.GetValue(HeldItemProperty);

        public StandaloneHeldItemComponent(string name = "standaloneHeldItem")
            : base(name)
        {
        }

        public void SetHeldItem(Slot value) =>
            AttachedObject.SetLocalValue(HeldItemProperty, value);

        Task IHandle<SetHeldItemIndex>.Handle(SetHeldItemIndex message)
        {
            return Task.CompletedTask;
        }

        Task<(int Index, Slot Slot)> IHandle<AskHeldItem, (int Index, Slot Slot)>.Handle(AskHeldItem message)
        {
            return Task.FromResult((0, HeldItem));
        }

        Task IHandle<SetHeldItem>.Handle(SetHeldItem message)
        {
            SetHeldItem(message.Slot);
            return Task.CompletedTask;
        }
    }
}
