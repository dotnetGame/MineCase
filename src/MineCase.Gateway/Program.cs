using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;

namespace MineCase.Gateway
{
    /// <summary>
    /// Orleans silo client.
    /// </summary>
    public class Program
    {
        public static int Main(string[] args)
        {
            return StartUp().Result;
        }

        private static async Task<int> StartUp()
        {
            var host = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ClusterClientHostedService>();
                    services.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
                    services.AddSingleton(_ => _.GetService<ClusterClientHostedService>().Client);

                    services.AddHostedService<ConnectionRouterClientHostedService>();

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