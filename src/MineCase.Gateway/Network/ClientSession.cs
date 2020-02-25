using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MineCase.Protocol;
using MineCase.Protocol.Handshaking.Server;
using MineCase.Protocol.Login.Server;
using MineCase.Protocol.Status.Server;
using MineCase.Server.Network;
using Orleans;
using ProtocolType = MineCase.Protocol.ProtocolType;

namespace MineCase.Gateway.Network
{
    public class ClientSession : IDisposable
    {
        private readonly Guid _sessionId = Guid.NewGuid();
        private TcpClient _tcpClient = null;
        private IClusterClient _client = null;
        private NetworkStream _dataStream = null;

        private bool disposed = false;

        private readonly ActionBlock<RawPacket> _outcomingPacketDispatcher = null;

        private IClientboundPacketObserver _clientboundPacketObserverRef = null;

        private readonly OutcomingPacketObserver _outcomingPacketObserver;

        public ClientSession(TcpClient tcpClient, IClusterClient clusterClient)
        {
            _tcpClient = tcpClient;
            _client = clusterClient;
            _outcomingPacketDispatcher = new ActionBlock<RawPacket>(SendOutcomingPacket);
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

        // Startup session
        public async Task Startup()
        {
            using (_dataStream = _tcpClient.GetStream())
            {
                // Subscribe observer to get packet from server
                _clientboundPacketObserverRef = await _client.CreateObjectReference<IClientboundPacketObserver>(_outcomingPacketObserver);
                var packetSink = _client.GetGrain<IClientboundPacketSink>(_sessionId);
                await packetSink.Subscribe(_clientboundPacketObserverRef);
                try
                {
                    DateTime expiredTime = DateTime.Now + TimeSpan.FromSeconds(30);
                    while (true)
                    {
                        await ProcessPacket();

                        // Renew subscribe, per 30 sec
                        if (DateTime.Now > expiredTime)
                        {
                            await _client.GetGrain<IClientboundPacketSink>(_sessionId).Subscribe(_clientboundPacketObserverRef);
                            expiredTime = DateTime.Now + TimeSpan.FromSeconds(30);
                        }
                    }
                }
                catch (EndOfStreamException)
                {
                    await _outcomingPacketDispatcher.Completion;
                }
            }
        }

        // Process raw packet
        private async Task ProcessPacket()
        {
            // Read raw packet
            RawPacket rawPacket = new RawPacket();
            await rawPacket.DeserializeAsync(_dataStream);

            var router = _client.GetGrain<IPacketRouter>(_sessionId);
            await router.ProcessPacket(rawPacket);
        }

        // Send Packet to game clients
        public async void DispatchOutcomingPacket(RawPacket packet)
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

        private async Task SendOutcomingPacket(RawPacket packet)
        {
            if (packet == null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Send);
                _outcomingPacketDispatcher.Complete();
            }
            else
            {
                // System.Console.WriteLine($"Send packet id:{_packetInfo.GetPacketId(packet):x2}, length: {rawPacket.Length}");
                await packet.SerializeAsync(_dataStream);
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

            public void ReceivePacket(RawPacket packet)
            {
                _session.DispatchOutcomingPacket(packet);
            }
        }
    }
}
