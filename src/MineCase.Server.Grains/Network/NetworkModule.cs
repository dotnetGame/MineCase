using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace MineCase.Server.Network
{
    internal class NetworkModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PacketRouterGrain>();
            builder.RegisterType<ClientboundPacketSinkGrain>();
            builder.RegisterType<PacketPackager>().As<IPacketPackager>();
        }
    }
}
