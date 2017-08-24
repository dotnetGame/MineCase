using Microsoft.Extensions.Logging;
using Orleans;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MineCase.Gateway.Network
{
    class ConnectionRouter
    {
        private readonly TcpListener _listener;
        private readonly IGrainFactory _grainFactory;
        private readonly ILogger _logger;

        public ConnectionRouter(IGrainFactory grainFactory, ILoggerFactory loggerFactory)
        {
            _grainFactory = grainFactory;
            _logger = loggerFactory.CreateLogger<ConnectionRouter>();
            _listener = new TcpListener(new IPEndPoint(IPAddress.Any, 25565));
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            _listener.Start();
            _logger.LogInformation("ConnectionRouter started.");
            while (!cancellationToken.IsCancellationRequested)
            {
                DispatchIncomingClient(await _listener.AcceptTcpClientAsync(), cancellationToken);
            }
            _listener.Stop();
        }

        private async void DispatchIncomingClient(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Incoming connection from {tcpClient.Client.RemoteEndPoint}.");
                using (var session = new ClientSession(tcpClient, _grainFactory))
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
