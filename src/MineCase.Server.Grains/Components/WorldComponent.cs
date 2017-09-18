using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Engine;
using MineCase.Server.World;

namespace MineCase.Server.Components
{
    internal class WorldComponent : Component
    {
        public static readonly DependencyProperty<IWorld> WorldProperty =
            DependencyProperty.Register<IWorld>("World", typeof(WorldComponent));

        public WorldComponent(string name = "world")
            : base(name)
        {
        }
    }

    public static class WorldComponentExtensions
    {
        public static IWorld GetWorld(this DependencyObject d) =>
            d.GetValue(WorldComponent.WorldProperty);

        public static bool TryGetWorld(this DependencyObject d, out IWorld value) =>
            d.TryGetLocalValue(WorldComponent.WorldProperty, out value);
    }
}
