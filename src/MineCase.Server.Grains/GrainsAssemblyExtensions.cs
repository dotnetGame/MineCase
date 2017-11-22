using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using MineCase.Engine;

namespace MineCase.Server
{
    public static class GrainsAssemblyExtensions
    {
        public static ICollection<Assembly> AddGrains(this ICollection<Assembly> assemblies)
        {
            var assembly = typeof(GrainsAssemblyExtensions).Assembly;
            assemblies.Add(assembly);
            DependencyProperty.OwnerTypeLoader = t => assembly.GetType(t, true);
            return assemblies;
        }
    }
}
