using MineCase.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Components
{
    internal class EntityLookComponent : Component
    {
        public static readonly DependencyProperty<float> PitchProperty =
            DependencyProperty.Register<float>("Pitch", typeof(EntityWorldPositionComponent));

        public static readonly DependencyProperty<float> YawProperty =
            DependencyProperty.Register<float>("Yaw", typeof(EntityWorldPositionComponent));

        public EntityLookComponent(string name = "entityLook")
            : base(name)
        {
        }

        public Task SetPitch(float value) =>
            AttachedObject.SetLocalValue(PitchProperty, value);

        public Task SetYaw(float value) =>
            AttachedObject.SetLocalValue(YawProperty, value);
    }
}
