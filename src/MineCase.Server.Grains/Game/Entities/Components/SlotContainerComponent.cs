using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class SlotContainerComponent : Component, IHandle<SetSlot>, IHandle<AskSlot, Slot>
    {
        public static readonly DependencyProperty<Slot[]> SlotsProperty =
            DependencyProperty.Register<Slot[]>("Slots", typeof(SlotContainerComponent));

        private readonly int _slotsCount;

        public event EventHandler<(int index, Slot slot)> SlotChanged;

        public SlotContainerComponent(int slotsCount, string name = "slotContainer")
            : base(name)
        {
            _slotsCount = slotsCount;
        }

        protected override void OnAttached()
        {
            var slots = AttachedObject.GetValue(SlotsProperty);
            if (slots == null || slots.Length != _slotsCount)
                AttachedObject.SetLocalValue(SlotsProperty, Enumerable.Repeat(Slot.Empty, _slotsCount).ToArray());
        }

        public Slot GetSlot(int index) =>
            AttachedObject.GetValue(SlotsProperty)[index];

        public void SetSlot(int index, Slot slot)
        {
            ref var old = ref AttachedObject.GetValue(SlotsProperty)[index];
            if (old != slot)
            {
                old = slot;
                MarkDirty();
                SlotChanged?.Invoke(this, (index, slot));
            }
        }

        public void SetSlots(Slot[] slots)
        {
            if (slots.Length != _slotsCount)
                throw new ArgumentException(nameof(slots));

            AttachedObject.SetLocalValue(SlotsProperty, slots);
            for (int i = 0; i < _slotsCount; i++)
                SlotChanged?.Invoke(this, (i, slots[i]));
        }

        Task IHandle<SetSlot>.Handle(SetSlot message)
        {
            SetSlot(message.Index, message.Slot);
            return Task.CompletedTask;
        }

        Task<Slot> IHandle<AskSlot, Slot>.Handle(AskSlot message) =>
            Task.FromResult(GetSlot(message.Index));

        private void MarkDirty()
        {
            AttachedObject.ValueStorage.IsDirty = true;
        }
    }
}
