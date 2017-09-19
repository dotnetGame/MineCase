using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Components
{
    internal class EntityOnGroundComponent : Component
    {
        public static readonly DependencyProperty<bool> IsOnGroundProperty =
            DependencyProperty.Register<bool>("IsOnGround", typeof(EntityWorldPositionComponent));

        public EntityOnGroundComponent(string name = "isOnGround")
            : base(name)
        {
        }

        public Task SetIsOnGround(bool value) =>
            AttachedObject.SetLocalValue(IsOnGroundProperty, value);
    }
}
