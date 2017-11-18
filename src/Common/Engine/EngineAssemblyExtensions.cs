using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MineCase
{
    /// <summary>
    /// Engine 程序集扩展
    /// </summary>
    public static class EngineAssemblyExtensions
    {
        /// <summary>
        /// 添加 Engine
        /// </summary>
        /// <param name="assemblies">程序集集合</param>
        /// <returns>程序集集合</returns>
        public static ICollection<Assembly> AddEngine(this ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(EngineAssemblyExtensions).Assembly);
            return assemblies;
        }
    }
}
