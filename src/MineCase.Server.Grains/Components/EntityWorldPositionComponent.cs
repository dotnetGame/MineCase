using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.World;

namespace MineCase.Server.Components
{
    internal class EntityWorldPositionComponent : Component
    {
        public static readonly DependencyProperty<EntityWorldPos> EntityWorldPositionProperty =
            DependencyProperty.Register<EntityWorldPos>("EntityWorldPosition", typeof(EntityWorldPositionComponent));

        public EntityWorldPositionComponent(string name = "entityWorldPosition")
            : base(name)
        {
        }

        public Task SetPosition(EntityWorldPos entityWorldPos)
            => AttachedObject.SetLocalValue(EntityWorldPositionProperty, entityWorldPos);
    }

    public static class EntityWorldPositionComponentExtensions
    {
        public static EntityWorldPos GetEntityWorldPosition(this DependencyObject d) =>
            d.GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);

        public static bool TryGetEntityWorldPosition(this DependencyObject d, out EntityWorldPos value) =>
            d.TryGetLocalValue(EntityWorldPositionComponent.EntityWorldPositionProperty, out value);
    }
}
