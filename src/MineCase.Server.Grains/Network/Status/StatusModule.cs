using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Network.Status
{
    internal class StatusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestGrain>();
            builder.RegisterType<PingGrain>();
        }
    }
}
