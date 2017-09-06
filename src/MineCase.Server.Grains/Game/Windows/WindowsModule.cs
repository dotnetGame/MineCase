using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Game.Windows
{
    internal class WindowsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InventoryWindowGrain>();
            builder.RegisterType<CraftingWindowGrain>();
        }
    }
}
