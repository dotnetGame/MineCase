using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    class WorldModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WorldGrain>();
            builder.RegisterType<WorldAccessorGrain>();
        }
    }
}
