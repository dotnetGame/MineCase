using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Engine.Data
{
    public interface IBigWorldValue
    {
    }

    /// <summary>
    /// BigWorldValue 接口.
    /// </summary>
    /// <typeparam name="T">值类型.</typeparam>
    public interface IBigWorldValue<T> : IBigWorldValue
    {
        /// <summary>
        /// 获取可否设置值.
        /// </summary>
        bool CanSetValue { get; }

        /// <summary>
        /// 获取值.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// 设置值.
        /// </summary>
        /// <param name="value">值.</param>
        void SetValue(T value);
    }
}
