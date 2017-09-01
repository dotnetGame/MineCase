using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.World
{
    internal class WorldModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WorldGrain>();
            builder.RegisterType<WorldAccessorGrain>();
            builder.RegisterType<ChunkColumnGrain>();
            builder.RegisterType<ChunkTrackingHub>();
            builder.RegisterType<CollectableFinder>();
        }
    }
}
