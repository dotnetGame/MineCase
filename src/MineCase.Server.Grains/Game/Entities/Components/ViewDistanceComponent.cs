using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class ViewDistanceComponent : Component
    {
        public static readonly DependencyProperty<byte> ViewDistanceProperty =
            DependencyProperty.Register("ViewDistance", typeof(ViewDistanceComponent), new PropertyMetadata<byte>(10));

        public ViewDistanceComponent(string name = "viewDistance")
            : base(name)
        {
        }
    }
}
