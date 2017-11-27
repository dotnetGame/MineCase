using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;

namespace MineCase.Server
{
    public static class GrainsAssemblyExtensions
    {
        public static ICollection<Assembly> AddGrains(this ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(GrainsAssemblyExtensions).Assembly);
            return assemblies;
        }
    }
}
