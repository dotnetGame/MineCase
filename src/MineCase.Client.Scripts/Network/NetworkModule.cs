using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MineCase.Client.Network
{
    internal class NetworkModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PacketPackager>().As<IPacketPackager>().SingleInstance();
            builder.RegisterType<ServerboundPacketSink>().As<IServerboundPacketSink>().As<IPacketSink>().InstancePerLifetimeScope();
            builder.RegisterType<PacketRouter>().As<IPacketRouter>().InstancePerLifetimeScope();
            builder.RegisterType<ClientSession>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<Handshaking.HandshakingHandler>().As<Handshaking.IHandshakingPacketGenerator>().InstancePerLifetimeScope();
            builder.RegisterType<Status.StatusHandler>().As<Status.IStatusPacketGenerator>().As<Status.IStatusHandler>().InstancePerLifetimeScope();
            builder.RegisterType<Login.LoginHandler>().As<Login.ILoginPacketGenerator>().As<Login.ILoginHandler>().InstancePerLifetimeScope();
        }
    }
}
