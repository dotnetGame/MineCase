using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Linq;
using Autofac.Extensions.DependencyInjection;
using Microsoft.IO;
using Autofac;
using MineCase.Server.Settings;
using Microsoft.Extensions.Hosting;

namespace MineCase.Server
{
    partial class Program
    {
        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddSingleton<RecyclableMemoryStreamManager>();
            services.Configure<PersistenceOptions>(context.Configuration.GetSection("persistenceOptions"));
        }

        private static void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, false);
        }

        private static void ConfigureAutofac(ContainerBuilder builder)
        {
            var assemblies = new List<Assembly>();
            assemblies
                .AddEngine()
                .AddInterfaces()
                .AddGrains();
            builder.RegisterAssemblyModules(assemblies.ToArray());
        }

        private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
        }
    }
}
