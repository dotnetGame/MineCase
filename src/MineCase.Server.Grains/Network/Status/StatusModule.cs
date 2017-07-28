using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Network.Status
{
    class StatusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestGrain>();
            builder.RegisterType<PingGrain>();
        }
    }
}
