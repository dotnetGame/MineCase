using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine
{
    /// <summary>
    /// 属性变更事件参数
    /// </summary>
    public class PropertyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 依赖属性
        /// </summary>
        public DependencyProperty Property { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="property">依赖属性</param>
        public PropertyChangedEventArgs(DependencyProperty property)
        {
            Property = property;
        }
    }

    /// <summary>
    /// 属性变更事件参数
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        /// <summary>
        /// 依赖属性
        /// </summary>
        public new DependencyProperty<T> Property => (DependencyProperty<T>)base.Property;

        /// <summary>
        /// 获取原始值
        /// </summary>
        public T OldValue { get; }

        /// <summary>
        /// 获取新值
        /// </summary>
        public T NewValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="property">依赖属性</param>
        /// <param name="oldValue">原始值</param>
        /// <param name="newValue">新值</param>
        public PropertyChangedEventArgs(DependencyProperty<T> property, T oldValue, T newValue)
            : base(property)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
