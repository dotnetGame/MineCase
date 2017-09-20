using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Data;

namespace MineCase.Engine
{
    public static class LocalDependencyValueExtensions
    {
        public static bool TryGetLocalValue<T>(this DependencyObject d, DependencyProperty<T> property, out T value)
        {
            return LocalDependencyValueProvider.Current.TryGetValue(property, d.ValueStorage, out value);
        }

        public static Task SetLocalValue<T>(this DependencyObject d, DependencyProperty<T> property, T value)
        {
            return LocalDependencyValueProvider.Current.SetValue(property, d.ValueStorage, value);
        }

        public static Task ClearLocalValue<T>(this DependencyObject d, DependencyProperty<T> property)
        {
            return LocalDependencyValueProvider.Current.ClearValue(property, d.ValueStorage);
        }
    }
}
