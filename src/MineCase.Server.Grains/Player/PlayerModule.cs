using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Player
{
    class PlayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NonAuthenticatedPlayerGrain>();
        }
    }
}
