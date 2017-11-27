using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Engine.Data
{
    /// <summary>
    /// 依赖值存储接口
    /// </summary>
    public interface IDependencyValueStorage
    {
        /// <summary>
        /// 获取或设置是否有脏数据
        /// </summary>
        bool IsDirty { get; set; }

        /// <summary>
        /// 当前值变更事件
        /// </summary>
        event
#if ECS_SERVER
        AsyncEventHandler<CurrentValueChangedEventArgs>
#else
        EventHandler<CurrentValueChangedEventArgs>
#endif
            CurrentValueChanged;

        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="provider">依赖值提供程序</param>
        /// <param name="key">依赖属性</param>
        /// <param name="addValueFactory">添加工厂</param>
        /// <param name="updateValueFactory">更新工厂</param>
        /// <returns>新的值</returns>
#if ECS_SERVER
        Task<IEffectiveValue> AddOrUpdate<T>(IDependencyValueProvider provider, DependencyProperty<T> key, Func<DependencyProperty, IEffectiveValue<T>> addValueFactory, Func<DependencyProperty, IEffectiveValue<T>, Task<IEffectiveValue<T>>> updateValueFactory);
#else
        IEffectiveValue AddOrUpdate<T>(IDependencyValueProvider provider, DependencyProperty<T> key, Func<DependencyProperty, IEffectiveValue<T>> addValueFactory, Func<DependencyProperty, IEffectiveValue<T>, IEffectiveValue<T>> updateValueFactory);
#endif

        /// <summary>
        /// 尝试获取值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="provider">依赖值提供程序</param>
        /// <param name="key">依赖属性</param>
        /// <param name="value">值</param>
        /// <returns>是否成功获取</returns>
        bool TryGetValue<T>(IDependencyValueProvider provider, DependencyProperty<T> key, out IEffectiveValue<T> value);

        /// <summary>
        /// 尝试删除值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="provider">依赖值提供程序</param>
        /// <param name="key">依赖属性</param>
        /// <param name="value">值</param>
        /// <returns>是否成功删除</returns>
#if ECS_SERVER
        Task<bool>
#else
        bool
#endif
            TryRemove<T>(IDependencyValueProvider provider, DependencyProperty<T> key, out IEffectiveValue<T> value);

        /// <summary>
        /// 尝试获取当前值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">依赖属性</param>
        /// <param name="value">值</param>
        /// <returns>是否成功获取</returns>
        bool TryGetCurrentValue<T>(DependencyProperty<T> key, out T value);

        /// <summary>
        /// 尝试获取当前 EffectiveValue
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">依赖属性</param>
        /// <param name="value">EffectiveValue</param>
        /// <returns>是否成功获取</returns>
        bool TryGetCurrentEffectiveValue<T>(DependencyProperty<T> key, out IEffectiveValue<T> value);

        /// <summary>
        /// 尝试获取当前 EffectiveValue
        /// </summary>
        /// <param name="key">依赖属性</param>
        /// <param name="value">EffectiveValue</param>
        /// <returns>是否成功获取</returns>
        bool TryGetCurrentEffectiveValue(DependencyProperty key, out IEffectiveValue value);

        /// <summary>
        /// 获取包含的依赖属性
        /// </summary>
        IEnumerable<DependencyProperty> Keys { get; }
    }
}
