using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.World.Generation
{
    internal class ChunkGeneratorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChunkGeneratorFlatGrain>();

            builder.RegisterType<ChunkGeneratorOverWorldGrain>();
        }
    }
}
