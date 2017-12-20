﻿using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Protocol;
using MineCase.Server.Game;
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
        private volatile bool _useCompression = false;
        private readonly Guid _sessionId;
        private readonly OutcomingPacketObserver _outcomingPacketObserver;
        private IClientboundPacketObserver _clientboundPacketObserverRef;
        private readonly ActionBlock<object> _outcomingPacketDispatcher;
        private readonly ObjectPool<UncompressedPacket> _uncompressedPacketObjectPool;
        private readonly IBufferPool<byte> _bufferPool;

        private readonly object _useCompressionPacket = new object();
        private uint _compressThreshold;

        public ClientSession(TcpClient tcpClient, IGrainFactory grainFactory, IBufferPool<byte> bufferPool, ObjectPool<UncompressedPacket> uncompressedPacketObjectPool)
        {
            _sessionId = Guid.NewGuid();
            _tcpClient = tcpClient;
            _grainFactory = grainFactory;
            _bufferPool = bufferPool;
            _uncompressedPacketObjectPool = uncompressedPacketObjectPool;
            _outcomingPacketObserver = new OutcomingPacketObserver(this);
            _outcomingPacketDispatcher = new ActionBlock<object>(SendOutcomingPacket);
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            using (_remoteStream = _tcpClient.GetStream())
            {
                _clientboundPacketObserverRef = await _grainFactory.CreateObjectReference<IClientboundPacketObserver>(_outcomingPacketObserver);
                await _grainFactory.GetGrain<IClientboundPacketSink>(_sessionId).Subscribe(_clientboundPacketObserverRef);
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
                    var router = _grainFactory.GetGrain<IPacketRouter>(_sessionId);
                    await router.Close();

                    await _outcomingPacketDispatcher.Completion;
                }
            }
        }

        private void OnClosed()
        {
            _outcomingPacketDispatcher.Post(null);
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
                        var compressedPacket = await CompressedPacket.DeserializeAsync(_remoteStream, null);
                        packet = PacketCompress.Decompress(compressedPacket, bufferScope, _compressThreshold, packet);
                    }
                    else
                    {
                        packet = await UncompressedPacket.DeserializeAsync(_remoteStream, bufferScope, packet);
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
                        await newPacket.SerializeAsync(_remoteStream);
                    }
                    else
                    {
                        await packet.SerializeAsync(_remoteStream);
                    }
                }
            }
        }

        private async Task DispatchIncomingPacket(UncompressedPacket packet)
        {
            var router = _grainFactory.GetGrain<IPacketRouter>(_sessionId);
            await router.SendPacket(packet);
        }

        private async void DispatchOutcomingPacket(object packet)
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

            public void UseCompression(uint threshold)
            {
                _session._compressThreshold = threshold;
                _session.DispatchOutcomingPacket(_session._useCompressionPacket);
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