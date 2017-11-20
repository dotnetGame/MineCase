using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine
{
    /// <summary>
    /// 依赖属性元数据
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class PropertyMetadata<T>
    {
        private bool _defaultValueSet;
        private T _defaultValue;

        /// <summary>
        /// 获取是否具有默认值
        /// </summary>
        public bool HasDefaultValue => _defaultValueSet;

        /// <summary>
        /// 获取默认值
        /// </summary>
        public T DefaultValue => _defaultValue;

        /// <summary>
        /// 属性更改事件
        /// </summary>
        public event
#if ECS_SERVER
        AsyncEventHandler<PropertyChangedEventArgs<T>>
#else
        EventHandler<PropertyChangedEventArgs<T>>
#endif
            PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata{T}"/> class.
        /// </summary>
        /// <param name="defaultValue">默认值</param>
        /// <param name="propertyChangedHandler">属性更改处理器</param>
        public PropertyMetadata(
            T defaultValue,
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs<T>>
#else
            EventHandler<PropertyChangedEventArgs<T>>
#endif
            propertyChangedHandler = null)
        {
            _defaultValue = defaultValue;
            _defaultValueSet = true;
            if (propertyChangedHandler != null)
                PropertyChanged += propertyChangedHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMetadata{T}"/> class.
        /// </summary>
        /// <param name="unsetValue">未设置默认值</param>
        /// <param name="propertyChangedHandler">属性更改处理器</param>
        public PropertyMetadata(
            DependencyProperty.UnsetValueType unsetValue,
#if ECS_SERVER
            AsyncEventHandler<PropertyChangedEventArgs<T>>
#else
            EventHandler<PropertyChangedEventArgs<T>>
#endif
            propertyChangedHandler = null)
        {
            _defaultValueSet = false;
            if (propertyChangedHandler != null)
                PropertyChanged += propertyChangedHandler;
        }

        /// <summary>
        /// 尝试获取默认值
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="property">依赖属性</param>
        /// <param name="value">值</param>
        /// <returns>是否具有默认值</returns>
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

        /// <summary>
        /// 尝试获取默认值
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="property">依赖属性</param>
        /// <param name="value">值</param>
        /// <returns>是否具有默认值</returns>
        protected virtual bool TryGetDefaultValueOverride(DependencyObject d, DependencyProperty<T> property, out T value)
        {
            value = default(T);
            return false;
        }

#if ECS_SERVER
        internal async Task RaisePropertyChanged(object sender, PropertyChangedEventArgs<T> e)
        {
            await OnPropertyChanged(sender, e);
            await PropertyChanged.InvokeSerial(sender, e);
        }
#else
        internal void RaisePropertyChanged(object sender, PropertyChangedEventArgs<T> e)
        {
            OnPropertyChanged(sender, e);
            PropertyChanged.InvokeSerial(sender, e);
        }
#endif

        /// <summary>
        /// 当属性修改时
        /// </summary>
        /// <param name="sender">发送方</param>
        /// <param name="e">参数</param>
#if ECS_SERVER
        protected virtual Task OnPropertyChanged(object sender, PropertyChangedEventArgs<T> e)
        {
            return Task.CompletedTask;
        }
#else
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs<T> e)
        {
        }
#endif

        /// <summary>
        /// 合并属性元数据
        /// </summary>
        /// <param name="old">原有元数据</param>
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
            {
                PropertyChanged =
#if ECS_SERVER
        (AsyncEventHandler<PropertyChangedEventArgs<T>>
#else
            (EventHandler<PropertyChangedEventArgs<T>>
#endif
)Delegate.Combine(old.PropertyChanged, PropertyChanged);
            }

            MergeOverride(old);
        }

        /// <summary>
        /// 尝试获取非默认值
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <param name="property">依赖属性</param>
        /// <param name="value">值</param>
        /// <returns>是否具有非默认值</returns>
        public virtual bool TryGetNonDefaultValue(DependencyObject d, DependencyProperty<T> property, out T value)
        {
            value = default(T);
            return false;
        }
    }

    internal static class EventHandlerExtensions
    {
        public static void InvokeSerial<TEventArgs>(this EventHandler<TEventArgs> handler, object sender, TEventArgs e)
        {
            handler?.Invoke(sender, e);
        }
    }
}
