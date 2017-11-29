using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class HealthComponent : Component
    {
        public static readonly DependencyProperty<uint> MaxHealthProperty =
            DependencyProperty.Register<uint>("MaxHealth", typeof(HealthComponent));

        public static readonly DependencyProperty<uint> HealthProperty =
            DependencyProperty.Register<uint>("Health", typeof(HealthComponent));

        public uint Health => AttachedObject.GetValue(HealthProperty);

        public uint MaxHealth => AttachedObject.GetValue(MaxHealthProperty);

        public HealthComponent(string name = "health")
            : base(name)
        {
        }

        public void SetHealth(uint value) =>
            AttachedObject.SetLocalValue(HealthProperty, value);

        public void SetMaxHealth(uint value) =>
            AttachedObject.SetLocalValue(MaxHealthProperty, value);
    }
}
