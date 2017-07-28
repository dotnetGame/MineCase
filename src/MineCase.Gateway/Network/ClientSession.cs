using MineCase.Protocol;
using MineCase.Server.Network;
using Orleans;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MineCase.Gateway.Network
{
    class ClientSession : IDisposable
    {
        private readonly TcpClient _tcpClient;
        private Stream _remoteStream;
        private readonly IGrainFactory _grainFactory;
        private bool _useCompression = false;
        private readonly Guid _sessionId;
        private readonly OutcomingPacketObserver _outcomingPacketObserver;
        private readonly ActionBlock<UncompressedPacket> _outcomingPacketDispatcher;

        public ClientSession(TcpClient tcpClient, IGrainFactory grainFactory)
        {
            _sessionId = Guid.NewGuid();
            _tcpClient = tcpClient;
            _grainFactory = grainFactory;
            _outcomingPacketObserver = new OutcomingPacketObserver(this);
            _outcomingPacketDispatcher = new ActionBlock<UncompressedPacket>(SendOutcomingPacket);
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            using (_remoteStream = _tcpClient.GetStream())
            {
                var observerRef = await _grainFactory.CreateObjectReference<IClientboundPacketObserver>(_outcomingPacketObserver);
                await _grainFactory.GetGrain<IClientboundPaketSink>(_sessionId).Subscribe(observerRef);
                try
                {
                    while (!cancellationToken.IsCancellationRequested &&
                        !_outcomingPacketDispatcher.Completion.IsCompleted)
                    {
                        await DispatchIncomingPacket();
                    }
                }
                catch (EndOfStreamException)
                {
                    await _outcomingPacketDispatcher.Completion;
                }
            }
        }

        private void OnClosed()
        {
            _outcomingPacketDispatcher.Complete();
            _tcpClient.Client.Shutdown(SocketShutdown.Send);
        }

        private async Task DispatchIncomingPacket()
        {
            UncompressedPacket packet;
            if (_useCompression)
            {
                var compressedPacket = await CompressedPacket.DeserializeAsync(_remoteStream);
                packet = PacketCompress.Decompress(ref compressedPacket);
            }
            else
            {
                packet = await UncompressedPacket.DeserializeAsync(_remoteStream);
            }
            await DispatchIncomingPacket(packet);
        }

        private async Task SendOutcomingPacket(UncompressedPacket packet)
        {
            if (_useCompression)
            {
                var newPacket = PacketCompress.Compress(ref packet);
                await newPacket.SerializeAsync(_remoteStream);
            }
            else
            {
                await packet.SerializeAsync(_remoteStream);
            }
        }

        private async Task DispatchIncomingPacket(UncompressedPacket packet)
        {
            var router = _grainFactory.GetGrain<IPacketRouter>(_sessionId);
            await router.SendPacket(packet);
        }

        private async void DispatchOutcomingPacket(UncompressedPacket packet)
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
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tcpClient.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
