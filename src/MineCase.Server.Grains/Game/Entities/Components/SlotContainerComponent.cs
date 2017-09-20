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

        public event AsyncEventHandler<(int index, Slot slot)> SlotChanged;

        public SlotContainerComponent(int slotsCount, string name = "slotContainer")
            : base(name)
        {
            _slotsCount = slotsCount;
        }

        protected override async Task OnAttached()
        {
            await AttachedObject.SetLocalValue(SlotsProperty, Enumerable.Repeat(Slot.Empty, _slotsCount).ToArray());
            await base.OnAttached();
        }

        public Slot GetSlot(int index) =>
            AttachedObject.GetValue(SlotsProperty)[index];

        public Task SetSlot(int index, Slot slot)
        {
            ref var old = ref AttachedObject.GetValue(SlotsProperty)[index];
            if (old != slot)
            {
                old = slot;
                return SlotChanged.InvokeSerial(this, (index, slot));
            }

            return Task.CompletedTask;
        }

        public async Task SetSlots(Slot[] slots)
        {
            if (slots.Length != _slotsCount)
                throw new ArgumentException(nameof(slots));

            await AttachedObject.SetLocalValue(SlotsProperty, slots);
            for (int i = 0; i < _slotsCount; i++)
                await SlotChanged.InvokeSerial(this, (i, slots[i]));
        }

        Task IHandle<SetSlot>.Handle(SetSlot message)
            => SetSlot(message.Index, message.Slot);

        Task<Slot> IHandle<AskSlot, Slot>.Handle(AskSlot message) =>
            Task.FromResult(GetSlot(message.Index));
    }
}
