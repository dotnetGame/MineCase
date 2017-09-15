using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    public interface IDependencyValueStorage
    {
        event AsyncEventHandler<CurrentValueChangedEventArgs> CurrentValueChanged;

        Task<IEffectiveValue> AddOrUpdate<T>(IDependencyValueProvider provider, DependencyProperty<T> key, Func<DependencyProperty, IEffectiveValue<T>> addValueFactory, Func<DependencyProperty, IEffectiveValue<T>, Task<IEffectiveValue<T>>> updateValueFactory);

        bool TryGetValue<T>(IDependencyValueProvider provider, DependencyProperty<T> key, out IEffectiveValue<T> value);

        Task<bool> TryRemove<T>(IDependencyValueProvider provider, DependencyProperty<T> key, out IEffectiveValue<T> value);

        bool TryGetCurrentValue<T>(DependencyProperty<T> key, out T value);

        bool TryGetCurrentEffectiveValue<T>(DependencyProperty<T> key, out IEffectiveValue<T> value);

        bool TryGetCurrentEffectiveValue(DependencyProperty key, out IEffectiveValue value);

        IEnumerable<DependencyProperty> Keys { get; }
    }
}
