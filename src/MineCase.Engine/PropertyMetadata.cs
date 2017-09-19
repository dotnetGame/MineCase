using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    public class PropertyMetadata<T>
    {
        private bool _defaultValueSet;
        private T _defaultValue;

        public bool HasDefaultValue => _defaultValueSet;

        public T DefaultValue => _defaultValue;

        public event AsyncEventHandler<PropertyChangedEventArgs<T>> PropertyChanged;

        public PropertyMetadata(T defaultValue, AsyncEventHandler<PropertyChangedEventArgs<T>> propertyChangedHandler = null)
        {
            _defaultValue = defaultValue;
            _defaultValueSet = true;
            if (propertyChangedHandler != null)
                PropertyChanged += propertyChangedHandler;
        }

        public PropertyMetadata(DependencyProperty.UnsetValueType unsetValue, AsyncEventHandler<PropertyChangedEventArgs<T>> propertyChangedHandler = null)
        {
            _defaultValueSet = false;
            if (propertyChangedHandler != null)
                PropertyChanged += propertyChangedHandler;
        }

        public bool TryGetDefaultValue(DependencyObject d, DependencyProperty<T> property, out T value)
        {
            if (TryGetDefaultValueOverride(d, property, out value))
                return true;
            if (_defaultValueSet)
            {
                value = _defaultValue;
                return true;
            }

            return false;
        }

        protected virtual bool TryGetDefaultValueOverride(DependencyObject d, DependencyProperty<T> property, out T value)
        {
            value = default(T);
            return false;
        }

        internal async Task RaisePropertyChanged(object sender, PropertyChangedEventArgs<T> e)
        {
            await OnPropertyChanged(sender, e);
            await PropertyChanged.InvokeSerial(sender, e);
        }

        protected virtual Task OnPropertyChanged(object sender, PropertyChangedEventArgs<T> e)
        {
            return Task.CompletedTask;
        }

        protected virtual void MergeOverride(PropertyMetadata<T> old)
        {
        }

        internal void Merge(PropertyMetadata<T> old, bool ownerIsDerived)
        {
            if (!_defaultValueSet && old._defaultValueSet)
            {
                _defaultValue = old._defaultValue;
                _defaultValueSet = true;
            }

            if (ownerIsDerived)
                PropertyChanged = (AsyncEventHandler<PropertyChangedEventArgs<T>>)Delegate.Combine(old.PropertyChanged, PropertyChanged);
            MergeOverride(old);
        }

        public virtual bool TryGetNonDefaultValue(DependencyObject d, DependencyProperty<T> property, out T value)
        {
            value = default(T);
            return false;
        }
    }
}
