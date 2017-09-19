using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class DraggedSlotComponent : Component, IHandle<SetDraggedSlot>, IHandle<AskDraggedSlot, Slot>
    {
        public static readonly DependencyProperty<Slot> DraggedSlotProperty =
            DependencyProperty.Register("DraggedSlot", typeof(DraggedSlotComponent), new PropertyMetadata<Slot>(Slot.Empty));

        public Slot DraggedSlot => AttachedObject.GetValue(DraggedSlotProperty);

        public DraggedSlotComponent(string name = "draggedSlot")
            : base(name)
        {
        }

        public Task SetDraggedSlot(Slot value) =>
            AttachedObject.SetLocalValue(DraggedSlotProperty, value);

        Task IHandle<SetDraggedSlot>.Handle(SetDraggedSlot message)
        {
            return SetDraggedSlot(message.Slot);
        }

        Task<Slot> IHandle<AskDraggedSlot, Slot>.Handle(AskDraggedSlot message)
        {
            return Task.FromResult(DraggedSlot);
        }
    }
}
