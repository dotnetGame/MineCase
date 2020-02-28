using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;

namespace MineCase.Server.Game.Entities.Components
{
    internal class FoodComponent : Component
    {
        public static readonly DependencyProperty<int> FoodProperty =
            DependencyProperty.Register<int>("Food", typeof(FoodComponent));

        public static readonly DependencyProperty<int> MaxFoodProperty =
            DependencyProperty.Register<int>("MaxFood", typeof(FoodComponent));

        public static readonly DependencyProperty<float> FoodSaturationProperty =
            DependencyProperty.Register<float>("FoodSaturation", typeof(FoodComponent));

        public int Food => AttachedObject.GetValue(FoodProperty);

        public int MaxFood => AttachedObject.GetValue(MaxFoodProperty);

        public float FoodSaturation => AttachedObject.GetValue(FoodSaturationProperty);

        public FoodComponent(string name = "food")
            : base(name)
        {
        }

        public void SetFood(int value) =>
            AttachedObject.SetLocalValue(FoodProperty, value);

        public void SetMaxFood(int value) =>
            AttachedObject.SetLocalValue(MaxFoodProperty, value);

        public void SetFoodSaturation(float value) =>
            AttachedObject.SetLocalValue(FoodSaturationProperty, value);
    }
}
