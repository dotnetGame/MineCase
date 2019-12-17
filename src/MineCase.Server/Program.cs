using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace MineCase.Server
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return StartUp().Result;
        }

        private static async Task<int> StartUp()
        {
            var connectionString = "mongodb://localhost/MineCase";
            var createShardKey = false;
            var host = new HostBuilder()
                .UseOrleans(builder =>
                {
                    // .UseLocalhostClustering()
                    builder
                        .UseMongoDBClient(connectionString)
                        .UseMongoDBClustering(options =>
                        {
                            options.DatabaseName = "MineCase";
                            options.CreateShardKeyForCosmos = createShardKey;
                        })
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "MineCaseApp";
                        })
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts => parts.AddFromApplicationBaseDirectory().WithReferences())
                        .UseMongoDBReminders(options =>
                        {
                            options.DatabaseName = "MineCase";
                            options.CreateShardKeyForCosmos = createShardKey;
                        })
                        .AddMongoDBGrainStorage("MongoDBStore", options =>
                        {
                            options.DatabaseName = "MineCase";
                            options.CreateShardKeyForCosmos = createShardKey;

                            options.ConfigureJsonSerializerSettings = (settings) =>
                            {
                                settings.NullValueHandling = NullValueHandling.Include;
                                settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
                                settings.DefaultValueHandling = DefaultValueHandling.Populate;
                            };
                        });
                })
                .ConfigureServices(services =>
                {
                    services.Configure<ConsoleLifetimeOptions>(options =>
                    {
                        options.SuppressStatusMessages = true;
                    });
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddConsole();
                })
                .RunConsoleAsync();

            await host;

            return 0;
        }
    }
}