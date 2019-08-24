using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Protocol;
using MineCase.Server.Network;
using Orleans;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

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
        private readonly ActionBlock<object> _outcomingPacketDispatcher;
        private readonly IBufferPool<byte> _bufferPool;
        private readonly ObjectPool<UncompressedPacket> _uncompressedPacketObjectPool;
        private volatile bool _useCompression = false;
        private readonly object _useCompressionPacket = new object();
        private uint _compressThreshold;
        private bool disposed = false;

        public ClientSession(TcpClient tcpClient, IClusterClient clusterClient)
        {
            _sessionId = Guid.NewGuid();
            _tcpClient = tcpClient;
            _client = clusterClient;
            _outcomingPacketDispatcher = new ActionBlock<object>(SendOutcomingPacket);
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

        public async void DispatchOutcomingPacket(object packet)
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
            var router = _client.GetGrain<IPacketRouter>(_sessionId);
            await router.SendPacket(packet);
        }

        public async Task Startup()
        {
            using (_dataStream = _tcpClient.GetStream())
            {
                // subscribe observer to get packet from server
                _clientboundPacketObserverRef = await _client.CreateObjectReference<IClientboundPacketObserver>(_outcomingPacketObserver);
                await _client.GetGrain<IClientboundPacketSink>(_sessionId).Subscribe(_clientboundPacketObserverRef);
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
            using (var bufferScope = _bufferPool.CreateScope())
            {
                var packet = _uncompressedPacketObjectPool.Get();
                try
                {
                    if (_useCompression)
                    {
                        var compressedPacket = await CompressedPacket.DeserializeAsync(_dataStream, null);
                        packet = PacketCompress.Decompress(compressedPacket, bufferScope, _compressThreshold, packet);
                    }
                    else
                    {
                        packet = await UncompressedPacket.DeserializeAsync(_dataStream, bufferScope, packet);
                    }
                    await DispatchIncomingPacket(packet);
                }
                finally
                {
                    _uncompressedPacketObjectPool.Return(packet);
                }
            }
        }

        private async Task SendOutcomingPacket(object packetOrCommand)
        {
            if (packetOrCommand == null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Send);
                _outcomingPacketDispatcher.Complete();
            }
            else if (packetOrCommand == _useCompressionPacket)
            {
                _useCompression = true;
            }
            else if (packetOrCommand is UncompressedPacket packet)
            {
                using (var bufferScope = _bufferPool.CreateScope())
                {
                    if (_useCompression)
                    {
                        var newPacket = PacketCompress.Compress(packet, bufferScope, _compressThreshold);
                        await newPacket.SerializeAsync(_dataStream);
                    }
                    else
                    {
                        await packet.SerializeAsync(_dataStream);
                    }
                }
            }
        }

        class OutcomingPacketObserver : IClientboundPacketObserver
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

            public void UseCompression(uint threshold)
            {
                _session._compressThreshold = threshold;
                _session.DispatchOutcomingPacket(_session._useCompressionPacket);
            }
        }
    }
    
}
