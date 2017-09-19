using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    internal class DependencyValueStorage : IDependencyValueStorage
    {
        private readonly Dictionary<DependencyProperty, SortedList<float, IEffectiveValue>> _dict = new Dictionary<DependencyProperty, SortedList<float, IEffectiveValue>>();

        public IEnumerable<DependencyProperty> Keys
        {
            get
            {
                foreach (var dp in _dict)
                {
                    var lst = dp.Value;
                    if (lst.Count != 0)
                        yield return dp.Key;
                }
            }
        }

        public event AsyncEventHandler<CurrentValueChangedEventArgs> CurrentValueChanged;

        public DependencyValueStorage()
        {
        }

        public async Task<IEffectiveValue> AddOrUpdate<T>(IDependencyValueProvider provider, DependencyProperty<T> key, Func<DependencyProperty, IEffectiveValue<T>> addValueFactory, Func<DependencyProperty, IEffectiveValue<T>, Task<IEffectiveValue<T>>> updateValueFactory)
        {
            var storage = GetStorage(provider, key);
            var priority = provider.Priority;
            IEffectiveValue result;
            var oldIdx = storage.IndexOfKey(priority);
            if (oldIdx == -1)
            {
                var value = addValueFactory(key);
                storage.Add(priority, value);
                value.ValueChanged = (s, e) => OnEffectiveValueChanged(priority, key, e.OldValue, e.NewValue);
                result = value;
                var raiseChanged = storage.IndexOfKey(priority) == 0;
                if (raiseChanged)
                    await OnCurrentValueChanged(key, false, null, true, value.Value);
            }
            else
            {
                var oldValue = (IEffectiveValue<T>)storage.Values[oldIdx];
                var newValue = await updateValueFactory(key, oldValue);
                if (oldValue != newValue)
                {
                    oldValue.ValueChanged = null;
                    newValue.ValueChanged = (s, e) => OnEffectiveValueChanged(priority, key, e.OldValue, e.NewValue);
                    storage[priority] = newValue;
                    var raiseChanged = oldIdx == 0;
                    if (raiseChanged)
                        await OnCurrentValueChanged(key, true, oldValue.Value, true, newValue.Value);
                }

                result = newValue;
            }

            return result;
        }

        public bool TryGetCurrentValue<T>(DependencyProperty<T> key, out T value)
        {
            SortedList<float, IEffectiveValue> list;
            if (_dict.TryGetValue(key, out list) && list.Count > 0)
            {
                value = ((IEffectiveValue<T>)list.Values[0]).Value;
                return true;
            }

            value = default(T);
            return false;
        }

        public bool TryGetCurrentEffectiveValue<T>(DependencyProperty<T> key, out IEffectiveValue<T> value)
        {
            IEffectiveValue eValue;
            if (TryGetCurrentEffectiveValue(key, out eValue))
            {
                value = (IEffectiveValue<T>)eValue;
                return true;
            }

            value = null;
            return false;
        }

        public bool TryGetCurrentEffectiveValue(DependencyProperty key, out IEffectiveValue value)
        {
            SortedList<float, IEffectiveValue> list;
            if (_dict.TryGetValue(key, out list) && list.Count > 0)
            {
                value = list.Values[0];
                return true;
            }

            value = null;
            return false;
        }

        private Task OnCurrentValueChanged(DependencyProperty key, bool hasOldValue, object oldValue, bool hasNewValue, object newValue)
        {
            return CurrentValueChanged.InvokeSerial(this, new CurrentValueChangedEventArgs(key, hasOldValue, oldValue, hasNewValue, newValue));
        }

        private Task OnEffectiveValueCleared(int index, DependencyProperty key, object oldValue)
        {
            if (index == 0)
            {
                bool hasNewValue = false;
                object newValue = null;
                SortedList<float, IEffectiveValue> list;
                if (_dict.TryGetValue(key, out list) && list.Count > 0)
                {
                    hasNewValue = true;
                    newValue = ((dynamic)list.Values[0]).Value;
                }

                return OnCurrentValueChanged(key, true, oldValue, hasNewValue, newValue);
            }

            return Task.CompletedTask;
        }

        private async Task OnEffectiveValueChanged(float priority, DependencyProperty key, object oldValue, object newValue)
        {
            SortedList<float, IEffectiveValue> list;
            if (_dict.TryGetValue(key, out list) && list.IndexOfKey(priority) == 0)
                await OnCurrentValueChanged(key, true, oldValue, true, newValue);
        }

        public bool TryGetValue<T>(IDependencyValueProvider provider, DependencyProperty<T> key, out IEffectiveValue<T> value)
        {
            var storage = GetStorage(provider, key);
            IEffectiveValue eValue;
            if (storage.TryGetValue(provider.Priority, out eValue))
            {
                value = (IEffectiveValue<T>)eValue;
                return true;
            }

            value = null;
            return false;
        }

        public Task<bool> TryRemove<T>(IDependencyValueProvider provider, DependencyProperty<T> key, out IEffectiveValue<T> value)
        {
            var storage = GetStorage(provider, key);
            var priority = provider.Priority;
            IEffectiveValue eValue;
            if (storage.TryGetValue(priority, out eValue))
            {
                value = (IEffectiveValue<T>)eValue;
                var index = storage.IndexOfKey(priority);
                storage.RemoveAt(index);
                return OnEffectiveValueCleared(index, key, value.Value)
                    .ContinueWith(t => true);
            }

            value = null;
            return Task.FromResult(false);
        }

        private SortedList<float, IEffectiveValue> GetStorage(IDependencyValueProvider provider, DependencyProperty key)
        {
            SortedList<float, IEffectiveValue> lst;
            if (!_dict.TryGetValue(key, out lst))
                _dict.Add(key, lst = new SortedList<float, IEffectiveValue>());
            return lst;
        }
    }

    public class CurrentValueChangedEventArgs : EventArgs
    {
        public DependencyProperty Property { get; }

        public object OldValue { get; }

        public object NewValue { get; }

        public bool HasOldValue { get; }

        public bool HasNewValue { get; }

        public CurrentValueChangedEventArgs(DependencyProperty property, bool hasOldValue, object oldValue, bool hasNewValue, object newValue)
        {
            Property = property;
            HasOldValue = hasOldValue;
            OldValue = oldValue;
            HasNewValue = hasNewValue;
            NewValue = newValue;
        }
    }
}
