using MineCase.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities.Components
{
    internal class HeldItemComponent : Component
    {
        public static readonly DependencyProperty<short> HeldItemIndexProperty =
            DependencyProperty.Register("HeldItemIndex", typeof(HeldItemComponent), new PropertyMetadata<short>(0));

        public HeldItemComponent(string name = "heldItem")
            : base(name)
        {
        }

        public Task SetHeldItemIndex(short index) =>
            AttachedObject.SetLocalValue(HeldItemIndexProperty, index);
    }
}
