using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Game
{
    internal class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChunkSenderGrain>();
            builder.RegisterType<ChunkSenderJobWorker>();
            builder.RegisterType<CraftingRecipesGrain>();
            builder.RegisterType<FurnaceRecipesGrain>();
        }
    }
}
