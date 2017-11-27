using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine.Data;

namespace MineCase.Engine
{
    /// <summary>
    /// 本地依赖值扩展
    /// </summary>
    public static class LocalDependencyValueExtensions
    {
        /// <summary>
        /// 尝试获取本地值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="d">依赖对象</param>
        /// <param name="property">依赖属性</param>
        /// <param name="value">值</param>
        /// <returns>是否获取成功</returns>
        public static bool TryGetLocalValue<T>(this DependencyObject d, DependencyProperty<T> property, out T value)
        {
            return LocalDependencyValueProvider.Current.TryGetValue(property, d.ValueStorage, out value);
        }

        /// <summary>
        /// 设置本地值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="d">依赖对象</param>
        /// <param name="property">依赖属性</param>
        /// <param name="value">值</param>
        public static void SetLocalValue<T>(this DependencyObject d, DependencyProperty<T> property, T value)
        {
            LocalDependencyValueProvider.Current.SetValue(property, d.ValueStorage, value);
        }

        /// <summary>
        /// 清除本地值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="d">依赖对象</param>
        /// <param name="property">依赖属性</param>
        public static void ClearLocalValue<T>(this DependencyObject d, DependencyProperty<T> property)
        {
            LocalDependencyValueProvider.Current.ClearValue(property, d.ValueStorage);
        }
    }
}
