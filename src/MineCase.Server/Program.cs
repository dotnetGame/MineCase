using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MineCase.Serialization.Serializers;
using Orleans;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using Orleans.Hosting;

namespace MineCase.Server
{
    partial class Program
    {
        static async Task Main(string[] args)
        {
            var createShardKey = false;
            Serializers.RegisterAll();

            var hostBuilder = new HostBuilder()
                .UseServiceProviderFactory(x => new AutofacServiceProviderFactory(ConfigureAutofac))
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging)
                .UseConsoleLifetime()
                .UseOrleans((Microsoft.Extensions.Hosting.HostBuilderContext context, ISiloBuilder builder) =>
                {
                    builder.Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "MineCaseService";
                    })
                    .Configure<SchedulingOptions>(options =>
                    {
                        options.AllowCallChainReentrancy = true;
                        options.PerformDeadlockDetection = true;
                    })
                    .ConfigureEndpoints(siloPort: Int32.Parse(context.Configuration.GetSection("endpoints")["siloPort"]), gatewayPort: Int32.Parse(context.Configuration.GetSection("endpoints")["gatewayPort"]))
                    .UseMongoDBClient(context.Configuration.GetSection("persistenceOptions")["connectionString"])
                    .AddSimpleMessageStreamProvider("JobsProvider")
                    .AddSimpleMessageStreamProvider("TransientProvider")
                    .UseMongoDBReminders(options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        options.CreateShardKeyForCosmos = createShardKey;
                    })
                    .UseMongoDBClustering(c =>
                    {
                        c.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        c.CreateShardKeyForCosmos = createShardKey;
                        // c.UseJsonFormat = true;
                    })
                    .ConfigureApplicationParts(ConfigureApplicationParts)
                    .UseDashboard(options => { })
                    .AddMongoDBGrainStorageAsDefault(c => c.Configure(options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        options.CreateShardKeyForCosmos = createShardKey;
                    }))
                    .AddMongoDBGrainStorage("PubSubStore", options =>
                    {
                        options.DatabaseName = context.Configuration.GetSection("persistenceOptions")["databaseName"];
                        options.CreateShardKeyForCosmos = createShardKey;
                    });
                });

            var host = hostBuilder.Build();
            Serializers.RegisterAll(host.Services);
            await host.RunAsync();
        }

        private static void ConfigureApplicationParts(IApplicationPartManager parts)
        {
            parts.AddFromApplicationBaseDirectory().WithReferences();
        }
    }
}