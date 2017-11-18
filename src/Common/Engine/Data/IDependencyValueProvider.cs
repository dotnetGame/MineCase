using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Engine.Data
{
    /// <summary>
    /// 依赖值提供程序接口
    /// </summary>
    public interface IDependencyValueProvider
    {
        /// <summary>
        /// 获取优先级
        /// </summary>
        float Priority { get; }
    }

    /// <summary>
    /// EffectiveValue 提供程序接口
    /// </summary>
    public interface IEffectiveValueProvider
    {
        /// <summary>
        /// 提供值
        /// </summary>
        /// <param name="d">依赖对象</param>
        /// <returns>EffectiveValue</returns>
        IEffectiveValue ProviderValue(DependencyObject d);
    }

    /// <summary>
    /// EffectiveValue 提供程序接口
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public interface IEffectiveValueProvider<T> : IEffectiveValueProvider
    {
    }
}
