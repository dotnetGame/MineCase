using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Orleans;

namespace MineCase.Gateway
{
    public class ConnectionRouterClientHostedService : IHostedService
    {
        private readonly IClusterClient _client;

        public ConnectionRouterClientHostedService(IClusterClient client, IApplicationLifetime lifetime)
        {
            _client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // example of calling grains from the initialized client
            // var friend = _client.GetGrain<IHello>(0);
            // var response = await friend.SayHello("Good morning, my friend!");
            // Console.WriteLine("\n\n{0}\n\n", response);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}