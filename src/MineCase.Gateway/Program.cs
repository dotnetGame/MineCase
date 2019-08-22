using Orleans;
using Orleans.ApplicationParts;
using Orleans.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using System.Threading;

namespace MineCase.Gateway
{
    class Program
    {
        private static int _retryAttempt = 0;

        private static readonly ManualResetEvent _exitEvent = new ManualResetEvent(false);

        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                Console.CancelKeyPress += (s, e) => {
                    e.Cancel = true;
                    _exitEvent.Set();
                };

                using (var client = await StartClientWithRetries())
                {
                    await ClientWork(client);
                    _exitEvent.WaitOne();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> StartClientWithRetries()
        {
            _retryAttempt = 0;
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "MineCaseService";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect(RetryFilter);
            Console.WriteLine("Client successfully connect to silo host");
            return client;
        }

        private static async Task<bool> RetryFilter(Exception exception)
        {
            _retryAttempt++;
            Console.WriteLine($"Cluster client attempt {_retryAttempt} failed to connect to cluster.");
            await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, _retryAttempt)));
            return true;
        }

        private static async Task ClientWork(IClusterClient client)
        {
           
        }

        private static void ConfigureApplicationParts(IApplicationPartManager parts)
        {
            parts.AddFromApplicationBaseDirectory().WithReferences();
        }
    }
}
