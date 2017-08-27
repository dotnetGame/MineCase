using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.World.Generation
{
    internal class ChunkGeneratorOverWorldModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChunkGeneratorOverWorldGrain>();
        }
    }
}
