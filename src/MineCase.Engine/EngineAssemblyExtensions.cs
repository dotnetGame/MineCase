using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MineCase
{
    public static class EngineAssemblyExtensions
    {
        public static ICollection<Assembly> AddEngine(this ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(EngineAssemblyExtensions).Assembly);
            return assemblies;
        }
    }
}
