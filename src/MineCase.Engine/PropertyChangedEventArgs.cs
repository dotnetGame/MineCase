using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    public class PropertyChangedEventArgs : EventArgs
    {
        public DependencyProperty Property { get; }

        public PropertyChangedEventArgs(DependencyProperty property)
        {
            Property = property;
        }
    }

    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        public new DependencyProperty<T> Property => (DependencyProperty<T>)base.Property;

        public T OldValue { get; }

        public T NewValue { get; }

        public PropertyChangedEventArgs(DependencyProperty<T> property, T oldValue, T newValue)
            : base(property)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
