using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Game.BlockEntities
{
    internal class BlockEntitiesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ChestBlockEntityGrain>();
        }
    }
}
