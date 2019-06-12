using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class DraggedSlotComponent : Component,
        IHandle<SetDraggedSlot>, IHandle<AskDraggedSlot, Slot>,
        IHandle<SetDraggedPath>, IHandle<AskDraggedPath, List<int>>
    {
        public static readonly DependencyProperty<Slot> DraggedSlotProperty =
            DependencyProperty.Register("DraggedSlot", typeof(DraggedSlotComponent), new PropertyMetadata<Slot>(Slot.Empty));

        public static readonly DependencyProperty<List<int>> DraggedPathProperty =
            DependencyProperty.Register("DraggedPath", typeof(DraggedSlotComponent), new PropertyMetadata<List<int>>(new List<int>()));

        public Slot DraggedSlot => AttachedObject.GetValue(DraggedSlotProperty);

        public List<int> DraggedPath => AttachedObject.GetValue(DraggedPathProperty);

        public DraggedSlotComponent(string name = "draggedSlot")
            : base(name)
        {
        }

        public void SetDraggedSlot(Slot value) =>
            AttachedObject.SetLocalValue(DraggedSlotProperty, value);

        Task IHandle<SetDraggedSlot>.Handle(SetDraggedSlot message)
        {
            SetDraggedSlot(message.Slot);
            return Task.CompletedTask;
        }

        Task<Slot> IHandle<AskDraggedSlot, Slot>.Handle(AskDraggedSlot message)
        {
            return Task.FromResult(DraggedSlot);
        }

        Task IHandle<SetDraggedPath>.Handle(SetDraggedPath message)
        {
            AttachedObject.SetLocalValue(DraggedPathProperty, message.Path);
            return Task.CompletedTask;
        }

        Task<List<int>> IHandle<AskDraggedPath, List<int>>.Handle(AskDraggedPath message)
        {
            return Task.FromResult(DraggedPath);
        }
    }
}
