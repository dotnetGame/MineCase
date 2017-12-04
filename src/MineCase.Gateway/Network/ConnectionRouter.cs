using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Protocol;
using MineCase.Server.Settings;
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
        private readonly IGrainFactory _grainFactory;
        private readonly ILogger _logger;
        private readonly IBufferPool<byte> _bufferPool;
        private readonly ObjectPool<UncompressedPacket> _uncompressedPacketObjectPool;

        public ConnectionRouter(IGrainFactory grainFactory, ILoggerFactory loggerFactory, IBufferPool<byte> bufferPool, ObjectPool<UncompressedPacket> uncompressedPacketObjectPool)
        {
            _grainFactory = grainFactory;
            _logger = loggerFactory.CreateLogger<ConnectionRouter>();
            _bufferPool = bufferPool;
            _uncompressedPacketObjectPool = uncompressedPacketObjectPool;
        }

        public async Task Startup(CancellationToken cancellationToken)
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

        private async void DispatchIncomingClient(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Incoming connection from {tcpClient.Client.RemoteEndPoint}.");
                using (var session = new ClientSession(tcpClient, _grainFactory, _bufferPool, _uncompressedPacketObjectPool))
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
