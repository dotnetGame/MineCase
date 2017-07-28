using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MineCase.Server
{
    public static class GrainsAssemblyExtensions
    {
        public static void AddGrains(this ContainerBuilder container)
        {
            container.RegisterAssemblyModules(typeof(GrainsAssemblyExtensions).GetTypeInfo().Assembly);
        }
    }
}
