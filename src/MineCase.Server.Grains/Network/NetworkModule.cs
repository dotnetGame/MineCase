using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Network
{
    class NetworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PacketRouterGrain>();
            builder.RegisterType<ClientboundPaketSinkGrain>();
        }
    }
}
