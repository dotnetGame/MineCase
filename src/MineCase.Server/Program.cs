using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using Orleans.Runtime.Configuration;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MineCase.Serialization.Serializers;
using Orleans;

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
            Serializers.RegisterAll();

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
            await StartAsync();
            Console.WriteLine("Press Ctrl+C to terminate...");
            Console.CancelKeyPress += (s, e) => _exitEvent.Set();
            _exitEvent.WaitOne();
            Console.WriteLine("Stopping...");
            await _siloHost.StopAsync();
            await _siloHost.Stopped;
            Console.WriteLine("Stopped.");
        }

        private static async Task StartAsync()
        {
            Serializers.RegisterAll(_siloHost.Services);
            await _siloHost.StartAsync();
        }

        private static ClusterConfiguration LoadClusterConfiguration()
        {
            var cluster = new ClusterConfiguration();
            cluster.LoadFromFile("OrleansConfiguration.dev.xml");
            cluster.AddMongoDBStorageProvider("PubSubStore", c =>
            {
                c.ConnectionString = Configuration.GetSection("persistenceOptions")["connectionString"];
                c.UseJsonFormat = true;
            });
            return cluster;
        }

        private static void ConfigureApplicationParts(ISiloHostBuilder builder)
        {
            foreach (var assembly in _assemblies)
                builder.AddApplicationPart(assembly);
        }
    }
}