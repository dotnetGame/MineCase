﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    /// <summary>
    /// 本地依赖值提供程序
    /// </summary>
    public class LocalDependencyValueProvider : IDependencyValueProvider
    {
        /// <summary>
        /// 获取当前提供程序
        /// </summary>
        public static LocalDependencyValueProvider Current { get; } = new LocalDependencyValueProvider();

        /// <inheritdoc/>
        public float Priority => 1.0f;

        /// <summary>
        /// 设置值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="storage">值存储</param>
        /// <param name="value">值</param>
#if ECS_SERVER
        public Task SetValue<T>(DependencyProperty<T> property, IDependencyValueStorage storage, T value)
        {
            return storage.AddOrUpdate(this, property, o => new LocalEffectiveValue<T>(value), async (k, o) =>
            {
                await ((LocalEffectiveValue<T>)o).SetValue(value);
                return o;
            });
        }
#else
        public void SetValue<T>(DependencyProperty<T> property, IDependencyValueStorage storage, T value)
        {
            storage.AddOrUpdate(this, property, o => new LocalEffectiveValue<T>(value), (k, o) =>
            {
                ((LocalEffectiveValue<T>)o).SetValue(value);
                return o;
            });
        }
#endif

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="storage">值存储</param>
        /// <param name="value">值</param>
        /// <returns>是否获取成功</returns>
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

        /// <summary>
        /// 清除值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="property">依赖属性</param>
        /// <param name="storage">值存储</param>
        public
#if ECS_SERVER
        Task
#else
        void
#endif
            ClearValue<T>(DependencyProperty<T> property, IDependencyValueStorage storage)
        {
            IEffectiveValue<T> eValue;
#if ECS_SERVER
            return
#endif
            storage.TryRemove(this, property, out eValue);
        }

        internal class LocalEffectiveValue<T> : IEffectiveValue<T>
        {
            /// <inheritdoc/>
            public
#if ECS_SERVER
        AsyncEventHandler<IEffectiveValueChangedEventArgs>
#else
        EventHandler<IEffectiveValueChangedEventArgs>
#endif
                ValueChanged { get; set; }

            /// <inheritdoc/>
            public bool CanSetValue => true;

            private T _value;

            /// <inheritdoc/>
            public T Value => _value;

            public LocalEffectiveValue(T value)
            {
                _value = value;
            }

            /// <inheritdoc/>
#if ECS_SERVER
            public async Task SetValue(T value)
#else
            public void SetValue(T value)
#endif
            {
                if (!EqualityComparer<T>.Default.Equals(_value, value))
                {
                    var oldValue = _value;
                    _value = value;
#if ECS_SERVER
                    await
#endif
                    ValueChanged.InvokeSerial(this, new EffectiveValueChangedEventArgs<T>(oldValue, value));
                }
            }
        }
    }
}