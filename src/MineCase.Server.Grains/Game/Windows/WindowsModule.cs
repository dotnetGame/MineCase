using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Windows
{
    class WindowsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InventoryWindowGrain>();
        }
    }
}
