using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Protocol;
using MineCase.Server.Network;
using Orleans;

namespace MineCase.Gateway.Network
{
    public class ClientSession : IDisposable
    {
        private readonly Guid _sessionId;
        private TcpClient _tcpClient = null;
        private IClusterClient _client = null;
        private NetworkStream _dataStream = null;
        private IClientboundPacketObserver _clientboundPacketObserverRef = null;
        private readonly OutcomingPacketObserver _outcomingPacketObserver;
        private readonly ActionBlock<UncompressedPacket> _outcomingPacketDispatcher;
        private volatile bool _useCompression = false;
        private uint _compressThreshold;
        private bool disposed = false;

        public ClientSession(TcpClient tcpClient, IClusterClient clusterClient)
        {
            _sessionId = Guid.NewGuid();
            _tcpClient = tcpClient;
            _client = clusterClient;
            _outcomingPacketDispatcher = new ActionBlock<UncompressedPacket>(SendOutcomingPacket);
            _outcomingPacketObserver = new OutcomingPacketObserver(this);
        }

        ~ClientSession()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_tcpClient != null)
                {
                    _tcpClient.Dispose();
                    _tcpClient = null;
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        public void OnClosed()
        {
        }

        public async void DispatchOutcomingPacket(UncompressedPacket packet)
        {
            try
            {
                if (!_outcomingPacketDispatcher.Completion.IsCompleted)
                    await _outcomingPacketDispatcher.SendAsync(packet);
            }
            catch
            {
                _outcomingPacketDispatcher.Complete();
            }
        }

        private async Task DispatchIncomingPacket(UncompressedPacket packet)
        {
            var packetRouter = _client.GetGrain<IPacketRouter>(_sessionId);
            await packetRouter.SendPacket(packet);
        }

        public async Task Startup()
        {
            using (_dataStream = _tcpClient.GetStream())
            {
                // subscribe observer to get packet from server
                _clientboundPacketObserverRef = await _client.CreateObjectReference<IClientboundPacketObserver>(_outcomingPacketObserver);
                var packetSink = _client.GetGrain<IClientboundPacketSink>(_sessionId);
                await packetSink.Subscribe(_clientboundPacketObserverRef);
                try
                {
                    DateTime expiredTime = DateTime.Now + TimeSpan.FromSeconds(30);
                    while (true)
                    {
                        await DispatchIncomingPacket();

                        // renew subscribe, 30 sec
                        if (DateTime.Now > expiredTime)
                        {
                            await _client.GetGrain<IClientboundPacketSink>(_sessionId).Subscribe(_clientboundPacketObserverRef);
                            expiredTime = DateTime.Now + TimeSpan.FromSeconds(30);
                        }
                    }
                }
                catch (EndOfStreamException)
                {
                    var router = _client.GetGrain<IPacketRouter>(_sessionId);
                    await router.Close();

                    await _outcomingPacketDispatcher.Completion;
                }
            }
        }

        private async Task DispatchIncomingPacket()
        {
            UncompressedPacket packet = null;
            if (_useCompression)
            {
                var compressedPacket = await CompressedPacket.DeserializeAsync(_dataStream);
                packet = PacketCompress.Decompress(compressedPacket, _compressThreshold);
            }
            else
            {
                packet = await UncompressedPacket.DeserializeAsync(_dataStream);
            }

            await DispatchIncomingPacket(packet);
        }

        private async Task SendOutcomingPacket(UncompressedPacket packet)
        {
            if (packet == null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Send);
                _outcomingPacketDispatcher.Complete();
            }
            else
            {
                if (_useCompression)
                {
                    var newPacket = PacketCompress.Compress(packet, _compressThreshold);
                    await newPacket.SerializeAsync(_dataStream);
                }
                else
                {
                    await packet.SerializeAsync(_dataStream);
                }
            }
        }

        private class OutcomingPacketObserver : IClientboundPacketObserver
        {
            private readonly ClientSession _session;

            public OutcomingPacketObserver(ClientSession session)
            {
                _session = session;
            }

            public void OnClosed()
            {
                _session.OnClosed();
            }

            public void ReceivePacket(UncompressedPacket packet)
            {
                _session.DispatchOutcomingPacket(packet);
            }
        }
    }
}
