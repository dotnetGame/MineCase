using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IO;
using MineCase.Buffers;
using MineCase.Engine.Builder;
using MineCase.Protocol;

[assembly: BootstrapperType(typeof(MineCase.Client.AppBootstrapper))]

namespace MineCase.Client
{
    internal class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureApplicationParts(ICollection<Assembly> assemblies)
        {
            assemblies.Add(typeof(AppBootstrapper).Assembly);
        }
    }

    internal class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultObjectPoolProvider>().As<ObjectPoolProvider>().SingleInstance();
            builder.Register<ObjectPool<UncompressedPacket>>(c =>
            {
                var provider = c.Resolve<ObjectPoolProvider>();
                return provider.Create<UncompressedPacket>();
            }).SingleInstance();
            builder.Register<IBufferPool<byte>>(c => new BufferPool<byte>(ArrayPool<byte>.Shared)).SingleInstance();
            builder.RegisterType<RecyclableMemoryStreamManager>();
        }
    }
}
