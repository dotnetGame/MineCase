using Microsoft.Extensions.Configuration;
using Orleans;
using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Logging;
using MineCase.Gateway.Network;
using Microsoft.Extensions.ObjectPool;
using MineCase.Protocol;
using MineCase.Buffers;
using System.Buffers;

namespace MineCase.Gateway
{
    class Program
    {
        public static IConfiguration Configuration { get; private set; }

        private static IClusterClient _clusterClient;
        private static readonly ManualResetEvent _exitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (s, e) => _exitEvent.Set();

            Configuration = LoadConfiguration();
            _clusterClient = new ClientBuilder()
                .LoadConfiguration("OrleansConfiguration.dev.xml")
                .ConfigureServices(ConfigureServices)
                .Build();
            Startup();
            _exitEvent.WaitOne();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ConfigureLogging());
            services.AddLogging();
            services.AddSingleton<ConnectionRouter>();
            ConfigureObjectPools(services);
        }

        private static ILoggerFactory ConfigureLogging()
        {
            var factory = new LoggerFactory();
            factory.AddConsole();

            return factory;
        }

        private static void ConfigureObjectPools(IServiceCollection services)
        {
            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<ObjectPool<UncompressedPacket>>(s =>
            {
                var provider = s.GetRequiredService<ObjectPoolProvider>();
                return provider.Create<UncompressedPacket>();
            });
            services.AddSingleton<IBufferPool<byte>>(s => new BufferPool<byte>(ArrayPool<byte>.Shared));
        }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", true, false);
            return builder.Build();
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