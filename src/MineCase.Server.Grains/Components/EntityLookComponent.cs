using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Components
{
    internal class EntityLookComponent : Component
    {
        public static readonly DependencyProperty<float> PitchProperty =
            DependencyProperty.Register<float>("Pitch", typeof(EntityLookComponent));

        public static readonly DependencyProperty<float> YawProperty =
            DependencyProperty.Register<float>("Yaw", typeof(EntityLookComponent));

        public static readonly DependencyProperty<float> HeadYawProperty =
            DependencyProperty.Register<float>("HeadYaw", typeof(EntityLookComponent));

        public float Pitch => AttachedObject.GetValue(PitchProperty);

        public float Yaw => AttachedObject.GetValue(YawProperty);

        public float HeadYaw => AttachedObject.GetValue(HeadYawProperty);

        public EntityLookComponent(string name = "entityLook")
            : base(name)
        {
        }

        public Task SetPitch(float value) =>
            AttachedObject.SetLocalValue(PitchProperty, value);

        public Task SetYaw(float value) =>
            AttachedObject.SetLocalValue(YawProperty, value);

        public Task SetHeadYaw(float value) =>
            AttachedObject.SetLocalValue(HeadYawProperty, value);
    }
}
