using Microsoft.Extensions.Configuration;
using Orleans;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Logging;
using MineCase.Gateway.Network;
using System.Reflection;
using System.Threading.Tasks;
using Orleans.Runtime;
using Polly;

namespace MineCase.Gateway
{
    partial class Program
    {
        public static IConfiguration Configuration { get; private set; }

        private static IClusterClient _clusterClient;
        private static readonly ManualResetEvent _exitEvent = new ManualResetEvent(false);
        private static Assembly[] _assemblies;

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (s, e) => _exitEvent.Set();
            Configuration = LoadConfiguration();
            Startup();
            _exitEvent.WaitOne();
            _clusterClient?.Dispose();
        }

        private static void ConfigureApplicationParts(IClientBuilder builder)
        {
            foreach (var assembly in _assemblies)
                builder.AddApplicationPart(assembly);
        }

        private static async void Startup()
        {
            ILogger logger = null;

            var retryPolicy = Policy.Handle<OrleansException>()
                .WaitAndRetryForeverAsync(
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (ex, timeSpan) => logger?.LogError($"Cluster connection failed. Next retry: {timeSpan.TotalSeconds} secs later."));
            await retryPolicy.ExecuteAsync(async () =>
            {
                _clusterClient?.Dispose();
                var builder = new ClientBuilder()
                    .LoadConfiguration("OrleansConfiguration.dev.xml")
                    .ConfigureServices(ConfigureServices)
                    .ConfigureLogging(ConfigureLogging);
                SelectAssemblies();
                ConfigureApplicationParts(builder);
                _clusterClient = builder.Build();

                var serviceProvider = _clusterClient.ServiceProvider;
                logger = _clusterClient.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();

                await Connect(logger);
            });

            var connectionRouter = _clusterClient.ServiceProvider.GetRequiredService<ConnectionRouter>();
            await connectionRouter.Startup(default(CancellationToken));
        }

        private static async Task Connect(ILogger logger)
        {
            logger.LogInformation("Connecting to cluster...");
            await _clusterClient.Connect();
            logger.LogInformation("Connected to cluster.");
        }
    }
}