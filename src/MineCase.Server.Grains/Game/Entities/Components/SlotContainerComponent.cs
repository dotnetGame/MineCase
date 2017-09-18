using MineCase.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities.Components
{
    internal class SlotContainerComponent : Component
    {
        public static readonly DependencyProperty<Slot[]> SlotsProperty =
            DependencyProperty.Register<Slot[]>("Slots", typeof(SlotContainerComponent));

        private readonly int _slotsCount;

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

        public void SetSlot(int index, Slot slot) =>
            AttachedObject.GetValue(SlotsProperty)[index] = slot;
    }
}
