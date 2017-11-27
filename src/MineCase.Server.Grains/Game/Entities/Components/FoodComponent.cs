using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class FoodComponent : Component
    {
        public static readonly DependencyProperty<uint> FoodProperty =
            DependencyProperty.Register<uint>("Food", typeof(FoodComponent));

        public static readonly DependencyProperty<uint> MaxFoodProperty =
            DependencyProperty.Register<uint>("MaxFood", typeof(FoodComponent));

        public static readonly DependencyProperty<float> FoodSaturationProperty =
            DependencyProperty.Register<float>("FoodSaturation", typeof(FoodComponent));

        public uint Food => AttachedObject.GetValue(FoodProperty);

        public uint MaxFood => AttachedObject.GetValue(MaxFoodProperty);

        public float FoodSaturation => AttachedObject.GetValue(FoodSaturationProperty);

        public FoodComponent(string name = "food")
            : base(name)
        {
        }

        public void SetFood(uint value) =>
            AttachedObject.SetLocalValue(FoodProperty, value);

        public void SetMaxFood(uint value) =>
            AttachedObject.SetLocalValue(MaxFoodProperty, value);

        public void SetFoodSaturation(float value) =>
            AttachedObject.SetLocalValue(FoodSaturationProperty, value);
    }
}
