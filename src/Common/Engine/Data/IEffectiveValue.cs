using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    /// <summary>
    /// EffectiveValue 接口
    /// </summary>
    public interface IEffectiveValue
    {
        /// <summary>
        /// 获取提供程序
        /// </summary>
        IDependencyValueProvider Provider { get; }

        /// <summary>
        /// 获取值改变处理器
        /// </summary>
        EventHandler<IEffectiveValueChangedEventArgs> ValueChanged { set; }
    }

    /// <summary>
    /// EffectiveValue 接口
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public interface IEffectiveValue<T> : IEffectiveValue
    {
        /// <summary>
        /// 获取可否设置值
        /// </summary>
        bool CanSetValue { get; }

        /// <summary>
        /// 获取值
        /// </summary>
        T Value { get; }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        void SetValue(T value);
    }

    /// <summary>
    /// 接口
    /// </summary>
    public interface IEffectiveValueChangedEventArgs
    {
        /// <summary>
        /// 获取原始值
        /// </summary>
        object OldValue { get; }

        /// <summary>
        /// 获取新值
        /// </summary>
        object NewValue { get; }
    }

    /// <summary>
    /// EffectiveValue 变更事件参数
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class EffectiveValueChangedEventArgs<T> : EventArgs, IEffectiveValueChangedEventArgs
    {
        /// <summary>
        /// 获取原始值
        /// </summary>
        public T OldValue { get; }

        /// <summary>
        /// 获取新值
        /// </summary>
        public T NewValue { get; }

        object IEffectiveValueChangedEventArgs.OldValue => OldValue;

        object IEffectiveValueChangedEventArgs.NewValue => NewValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectiveValueChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="oldValue">原始值</param>
        /// <param name="newValue">新值</param>
        public EffectiveValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
