using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    /// <summary>
    /// 依赖值存储 - CRUD
    /// </summary>
    internal partial class DependencyValueStorage : IDependencyValueStorage
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

        public event
#if ECS_SERVER
        AsyncEventHandler<CurrentValueChangedEventArgs>
#else
        EventHandler<CurrentValueChangedEventArgs>
#endif
            CurrentValueChanged;

        public DependencyValueStorage()
        {
        }

        public
#if ECS_SERVER
        async Task<IEffectiveValue> AddOrUpdate<T>(IDependencyValueProvider provider, DependencyProperty<T> key, Func<DependencyProperty, IEffectiveValue<T>> addValueFactory, Func<DependencyProperty, IEffectiveValue<T>, Task<IEffectiveValue<T>>> updateValueFactory)
#else
        IEffectiveValue AddOrUpdate<T>(IDependencyValueProvider provider, DependencyProperty<T> key, Func<DependencyProperty, IEffectiveValue<T>> addValueFactory, Func<DependencyProperty, IEffectiveValue<T>, IEffectiveValue<T>> updateValueFactory)
#endif
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
                {
#if ECS_SERVER
                await
#endif
                    OnCurrentValueChanged(key, false, null, true, value.Value);
                }
            }
            else
            {
                var oldValue = (IEffectiveValue<T>)storage.Values[oldIdx];
                var newValue =
#if ECS_SERVER
                await
#endif
                    updateValueFactory(key, oldValue);
                if (oldValue != newValue)
                {
                    oldValue.ValueChanged = null;
                    newValue.ValueChanged = (s, e) => OnEffectiveValueChanged(priority, key, e.OldValue, e.NewValue);
                    storage[priority] = newValue;
                    var raiseChanged = oldIdx == 0;
                    if (raiseChanged)
                    {
#if ECS_SERVER
                    await
#endif
                        OnCurrentValueChanged(key, true, oldValue.Value, true, newValue.Value);
                    }
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

        private
#if ECS_SERVER
        Task
#else
        void
#endif
            OnCurrentValueChanged(DependencyProperty key, bool hasOldValue, object oldValue, bool hasNewValue, object newValue)
        {
#if ECS_SERVER
            return
#endif
            CurrentValueChanged.InvokeSerial(this, new CurrentValueChangedEventArgs(key, hasOldValue, oldValue, hasNewValue, newValue));
        }

        private
#if ECS_SERVER
        Task
#else
        void
#endif
            OnEffectiveValueCleared(int index, DependencyProperty key, object oldValue)
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

#if ECS_SERVER
                return
#endif
                OnCurrentValueChanged(key, true, oldValue, hasNewValue, newValue);
            }

#if ECS_SERVER
            return Task.CompletedTask;
#endif
        }

        private
#if ECS_SERVER
        Task
#else
        void
#endif
            OnEffectiveValueChanged(float priority, DependencyProperty key, object oldValue, object newValue)
        {
            SortedList<float, IEffectiveValue> list;
            if (_dict.TryGetValue(key, out list) && list.IndexOfKey(priority) == 0)
            {
#if ECS_SERVER
            return
#endif
                OnCurrentValueChanged(key, true, oldValue, true, newValue);
            }
#if ECS_SERVER
            return Task.CompletedTask;
#endif
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

        public
#if ECS_SERVER
        Task<bool>
#else
        bool
#endif
            TryRemove<T>(IDependencyValueProvider provider, DependencyProperty<T> key, out IEffectiveValue<T> value)
        {
            var storage = GetStorage(provider, key);
            var priority = provider.Priority;
            IEffectiveValue eValue;
            if (storage.TryGetValue(priority, out eValue))
            {
                value = (IEffectiveValue<T>)eValue;
                var index = storage.IndexOfKey(priority);
                storage.RemoveAt(index);
#if ECS_SERVER
                return OnEffectiveValueCleared(index, key, value.Value)
                    .ContinueWith(t => true);
#else
                OnEffectiveValueCleared(index, key, value.Value);
                return true;
#endif
            }

            value = null;
#if ECS_SERVER
            return Task.FromResult(false);
#else
            return false;
#endif
        }

        private SortedList<float, IEffectiveValue> GetStorage(IDependencyValueProvider provider, DependencyProperty key)
        {
            SortedList<float, IEffectiveValue> lst;
            if (!_dict.TryGetValue(key, out lst))
                _dict.Add(key, lst = new SortedList<float, IEffectiveValue>());
            return lst;
        }
    }

    /// <summary>
    /// 当前值变更
    /// </summary>
    public class CurrentValueChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 获取依赖属性
        /// </summary>
        public DependencyProperty Property { get; }

        /// <summary>
        /// 获取原始值
        /// </summary>
        public object OldValue { get; }

        /// <summary>
        /// 获取新值
        /// </summary>
        public object NewValue { get; }

        /// <summary>
        /// 获取是否有原始值
        /// </summary>
        public bool HasOldValue { get; }

        /// <summary>
        /// 获取是否有新值
        /// </summary>
        public bool HasNewValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentValueChangedEventArgs"/> class.
        /// </summary>
        /// <param name="property">依赖属性</param>
        /// <param name="hasOldValue">是否有原始值</param>
        /// <param name="oldValue">原始值</param>
        /// <param name="hasNewValue">是否有新值</param>
        /// <param name="newValue">新值</param>
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
