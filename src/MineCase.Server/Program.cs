using Microsoft.Extensions.Configuration;
using Orleans.Hosting;
using Orleans.Runtime.Configuration;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MineCase.Serialization.Serializers;
using Orleans;
using Orleans.Configuration;
using Orleans.ApplicationParts;

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

            var createShardKey = false;

            SelectAssemblies();
            var builder = new SiloHostBuilder()
                .ConfigureLogging(ConfigureLogging)
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MineCaseService";
                })
                .Configure<SchedulingOptions>(options =>
                {
                    options.AllowCallChainReentrancy = true;
                    options.PerformDeadlockDetection = true;
                })
                .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
                .UseMongoDBClient(Configuration.GetSection("persistenceOptions")["connectionString"])
                .AddSimpleMessageStreamProvider("JobsProvider")
                .AddSimpleMessageStreamProvider("TransientProvider")
                .UseMongoDBReminders(options =>
                {
                    options.DatabaseName = Configuration.GetSection("persistenceOptions")["databaseName"];
                    options.CreateShardKeyForCosmos = createShardKey;
                })
                .UseMongoDBClustering(c =>
                {
                    c.DatabaseName = Configuration.GetSection("persistenceOptions")["databaseName"];
                    c.CreateShardKeyForCosmos = createShardKey;
                    // c.UseJsonFormat = true;
                })
                .ConfigureApplicationParts(ConfigureApplicationParts)
                .UseDashboard(options => { })
                .UseServiceProviderFactory(ConfigureServices);

            MongoDBSiloExtensions.AddMongoDBGrainStorageAsDefault(builder, options => {
                options.DatabaseName = Configuration.GetSection("persistenceOptions")["databaseName"];
                options.CreateShardKeyForCosmos = createShardKey;
            });

            MongoDBSiloExtensions.AddMongoDBGrainStorage(builder, "PubSubStore", options => {
                options.DatabaseName = Configuration.GetSection("persistenceOptions")["databaseName"];
                options.CreateShardKeyForCosmos = createShardKey;
            });

            // ConfigureApplicationParts(builder);
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
        
        /*
        private static ClusterConfiguration LoadClusterConfiguration()
        {
            var cluster = new ClusterConfiguration();
            cluster.LoadFromFile("OrleansConfiguration.dev.xml");
            cluster.RegisterDashboard();
            cluster.AddMongoDBStorageProvider();
            return cluster;
        }
        */

        private static void ConfigureApplicationParts(IApplicationPartManager parts)
        {
            //foreach (var assembly in _assemblies)
            //    parts.AddApplicationPart(assembly);
            parts.AddFromApplicationBaseDirectory().WithReferences();
        }
    }
}