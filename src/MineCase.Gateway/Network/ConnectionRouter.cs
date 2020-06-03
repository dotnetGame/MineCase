using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Protocol;
using MineCase.Server.Settings;
using Orleans;

namespace MineCase.Gateway.Network
{
    class ConnectionRouter : IHostedService
    {
        private readonly IOrleansClient _grainFactory;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public ConnectionRouter(IOrleansClient grainFactory, ILogger<ConnectionRouter> logger, IServiceProvider serviceProvider)
        {
            _grainFactory = grainFactory;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var settings = await _grainFactory.GetGrain<IServerSettings>(0).GetSettings();
                IPAddress ip = IPAddress.Parse(settings.ServerIp);
                int port = (int)settings.ServerPort;

                TcpListener _listener;
                _listener = new TcpListener(new IPEndPoint(ip, port));
                _listener.Start();
                _logger.LogInformation("ConnectionRouter started.");
                while (!cancellationToken.IsCancellationRequested)
                {
                    DispatchIncomingClient(await _listener.AcceptTcpClientAsync(), cancellationToken);
                }
                _listener.Stop();
            }
            catch (FormatException)
            {
                _logger.LogError($"The configuration of gateway have an incorrect format.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void DispatchIncomingClient(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Incoming connection from {tcpClient.Client.RemoteEndPoint}.");
                using (var session = ActivatorUtilities.CreateInstance<ClientSession>(_serviceProvider, tcpClient))
                {
                    await session.Startup(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId), ex, ex.Message);
            }
        }
    }
}
