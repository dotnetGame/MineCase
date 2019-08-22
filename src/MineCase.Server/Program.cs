using Orleans;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace MineCase.Server
{
    class Program
    {
        private static readonly ManualResetEvent _exitEvent = new ManualResetEvent(false);

        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                // build silo server
                var host = BuildSilo();
                // run silo server
                await host.StartAsync();
                Console.WriteLine("Press Ctrl+C to terminate...");
                Console.CancelKeyPress += (s, e) => {
                    e.Cancel = true;
                    _exitEvent.Set();
                };
                // wait server exit or get ctrl+c key
                _exitEvent.WaitOne();

                // stop silo server
                Console.WriteLine("Stopping...");
                await host.StopAsync();
                await host.Stopped;
                Console.WriteLine("Stopped.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
                return 1;
            }
            
            return 0;
        }

        private static ISiloHost BuildSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MineCaseService";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(ConfigureApplicationParts)
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            return host;
        }

        private static void ConfigureApplicationParts(IApplicationPartManager parts)
        {
            parts.AddFromApplicationBaseDirectory().WithReferences();
        }
    }
}
