using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using Orleans.Runtime.Configuration;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MineCase.Server
{
    partial class Program
    {
        private static readonly ManualResetEvent _exitEvent = new ManualResetEvent(false);
        private static ISiloHost _siloHost;
        private static Assembly[] _assemblies;

        public static IConfiguration Configuration { get; private set; }

        static async Task Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            ConfigureAppConfiguration(configBuilder);
            Configuration = configBuilder.Build();

            var builder = new SiloHostBuilder()
                .ConfigureLogging(ConfigureLogging)
                .UseConfiguration(LoadClusterConfiguration())
                .UseServiceProviderFactory(ConfigureServices);
            SelectAssemblies();
            ConfigureApplicationParts(builder);
            _siloHost = builder.Build();
            await _siloHost.StartAsync();
            Console.WriteLine("Press Ctrl+C to terminate...");
            Console.CancelKeyPress += (s, e) => _exitEvent.Set();
            _exitEvent.WaitOne();
            await _siloHost.StopAsync();
        }

        private static ClusterConfiguration LoadClusterConfiguration()
        {
            var cluster = new ClusterConfiguration();
            cluster.LoadFromFile("OrleansConfiguration.dev.xml");
            cluster.AddMemoryStorageProvider();
            return cluster;
        }

        private static void ConfigureApplicationParts(ISiloHostBuilder builder)
        {
            foreach (var assembly in _assemblies)
                builder.AddApplicationPart(assembly);
        }
    }
}