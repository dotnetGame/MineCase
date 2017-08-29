using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;

namespace MineCase.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            Configuration = LoadConfiguration();
        }

        private IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", true, false);
            return builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton(ConfigureLogging());
            services.AddLogging();
            services.AddSingleton<RecyclableMemoryStreamManager>();

            var container = new ContainerBuilder();
            container.Populate(services);
            container.AddGrains();
            return new AutofacServiceProvider(container.Build());
        }

        private static ILoggerFactory ConfigureLogging()
        {
            var factory = new LoggerFactory();
            factory.AddConsole();

            return factory;
        }
    }
}