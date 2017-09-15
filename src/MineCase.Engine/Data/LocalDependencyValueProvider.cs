using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    public class LocalDependencyValueProvider : IDependencyValueProvider
    {
        public static LocalDependencyValueProvider Current { get; } = new LocalDependencyValueProvider();

        public float Priority => 1.0f;

        public Task SetValue<T>(DependencyProperty<T> property, IDependencyValueStorage storage, T value)
        {
            return storage.AddOrUpdate(this, property, o => new LocalEffectiveValue<T>(value), async (k, o) =>
            {
                await ((LocalEffectiveValue<T>)o).SetValue(value);
                return o;
            });
        }

        public bool TryGetValue<T>(DependencyProperty<T> property, IDependencyValueStorage storage, out T value)
        {
            IEffectiveValue<T> eValue;
            if (storage.TryGetValue(this, property, out eValue))
            {
                value = eValue.Value;
                return true;
            }

            value = default(T);
            return false;
        }

        public Task ClearValue<T>(DependencyProperty<T> property, IDependencyValueStorage storage)
        {
            IEffectiveValue<T> eValue;
            return storage.TryRemove(this, property, out eValue);
        }

        internal class LocalEffectiveValue<T> : IEffectiveValue<T>
        {
            public AsyncEventHandler<IEffectiveValueChangedEventArgs> ValueChanged { get; set; }

            public bool CanSetValue => true;

            private T _value;

            public T Value => _value;

            public LocalEffectiveValue(T value)
            {
                _value = value;
            }

            public async Task SetValue(T value)
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    var oldValue = _value;
                    _value = value;
                    await ValueChanged.InvokeSerial(this, new EffectiveValueChangedEventArgs<T>(oldValue, value));
                }
            }
        }
    }
}