using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Game.Entities
{
    internal class EntitiesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PlayerGrain>();
            builder.RegisterType<PickupGrain>();
            builder.RegisterType<MobGrain>();
        }
    }
}
