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

namespace MineCase.Server
{
    partial class Program
    {
        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddLogging();
            services.AddSingleton<RecyclableMemoryStreamManager>();
            services.Configure<PersistenceOptions>(Configuration.GetSection("persistenceOptions"));

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterAssemblyModules(_assemblies);
            return new AutofacServiceProvider(container.Build());
        }

        private static void ConfigureAppConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, false);
        }

        private static void SelectAssemblies()
        {
            var assemblies = new List<Assembly>();
            assemblies
                .AddEngine()
                .AddInterfaces()
                .AddGrains();
            _assemblies = assemblies.ToArray();
        }

        private static void ConfigureLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddConsole();
        }
    }
}
