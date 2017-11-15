using Microsoft.Extensions.Configuration;
using Orleans;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Logging;
using MineCase.Gateway.Network;
using System.Reflection;

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
            var builder = new ClientBuilder()
                .LoadConfiguration("OrleansConfiguration.dev.xml")
                .ConfigureServices(ConfigureServices)
                .ConfigureLogging(ConfigureLogging);
            SelectAssemblies();
            ConfigureApplicationParts(builder);
            _clusterClient = builder.Build();
            Startup();
            _exitEvent.WaitOne();
        }

        private static void ConfigureApplicationParts(IClientBuilder builder)
        {
            foreach (var assembly in _assemblies)
                builder.AddApplicationPart(assembly);
        }

        private static async void Startup()
        {
            var serviceProvider = _clusterClient.ServiceProvider;
            var logger = _clusterClient.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
            try
            {
                logger.LogInformation("Connecting to cluster...");
                await _clusterClient.Connect();
                logger.LogInformation("Connected to cluster.");
            }
            catch (Exception e)
            {
                logger.LogError("Cluster connection failed.\n" + "Exception stack trace:" + e.StackTrace);
                _exitEvent.Set();
                return;
            }
            var connectionRouter = serviceProvider.GetRequiredService<ConnectionRouter>();
            await connectionRouter.Startup(default(CancellationToken));
        }
    }
}