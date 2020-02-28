using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class HealthComponent : Component
    {
        public static readonly DependencyProperty<int> MaxHealthProperty =
            DependencyProperty.Register<int>("MaxHealth", typeof(HealthComponent));

        public static readonly DependencyProperty<int> HealthProperty =
            DependencyProperty.Register<int>("Health", typeof(HealthComponent));

        public int Health => AttachedObject.GetValue(HealthProperty);

        public int MaxHealth => AttachedObject.GetValue(MaxHealthProperty);

        public HealthComponent(string name = "health")
            : base(name)
        {
        }

        public void SetHealth(int value) =>
            AttachedObject.SetLocalValue(HealthProperty, value);

        public void SetMaxHealth(int value) =>
            AttachedObject.SetLocalValue(MaxHealthProperty, value);
    }
}
