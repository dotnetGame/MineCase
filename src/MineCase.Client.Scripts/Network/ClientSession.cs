using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Autofac;
using Microsoft.Extensions.ObjectPool;
using MineCase.Buffers;
using MineCase.Protocol;
using UnityEngine;

namespace MineCase.Client.Network
{
    public class ClientSession
    {
        private readonly TcpClient _tcpClient;
        private Stream _remoteStream;
        private bool _useCompression = false;
        private readonly ObjectPool<UncompressedPacket> _uncompressedPacketObjectPool;
        private readonly IBufferPool<byte> _bufferPool;
        private readonly IPacketRouter _packetRouter;
        private readonly OutcomingPacketObserver _outcomingPacketObserver;
        private readonly ActionBlock<UncompressedPacket> _outcomingPacketDispatcher;

        public SessionScope SessionScope { get; }

        public event EventHandler Connected;

        public ClientSession(SessionScope sessionScope, IPacketRouter packetRouter, IBufferPool<byte> bufferPool, ObjectPool<UncompressedPacket> uncompressedPacketObjectPool)
        {
            SessionScope = sessionScope;
            _tcpClient = new TcpClient();
            _bufferPool = bufferPool;
            _uncompressedPacketObjectPool = uncompressedPacketObjectPool;
            _packetRouter = packetRouter;
            _outcomingPacketObserver = new OutcomingPacketObserver(this);
            _outcomingPacketDispatcher = new ActionBlock<UncompressedPacket>(SendOutcomingPacket);
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            await _tcpClient.ConnectAsync(IPAddress.Loopback, 25565).ConfigureAwait(false);
            using (_remoteStream = _tcpClient.GetStream())
            {
                SessionScope.ServiceProvider.Resolve<IServerboundPacketSink>().Subscribe(_outcomingPacketObserver);
                Connected?.Invoke(this, EventArgs.Empty);
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        await DispatchIncomingPacket();
                    }
                }
                catch (EndOfStreamException)
                {
                    Debug.Log("Connection closed.");
                }
            }
        }

        private void OnClosed()
        {
            _outcomingPacketDispatcher.Post(null);
        }

        private async Task SendOutcomingPacket(UncompressedPacket packet)
        {
            // Close
            if (packet == null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Send);
                _outcomingPacketDispatcher.Complete();
            }
            else if (_useCompression)
            {
                var newPacket = PacketCompress.Compress(ref packet);
                await newPacket.SerializeAsync(_remoteStream);
            }
            else
            {
                await packet.SerializeAsync(_remoteStream);
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
                        var compressedPacket = await CompressedPacket.DeserializeAsync(_remoteStream, null);
                        packet = PacketCompress.Decompress(ref compressedPacket);
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

        private async Task DispatchIncomingPacket(UncompressedPacket packet)
        {
            await _packetRouter.SendPacket(packet);
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

        private class OutcomingPacketObserver : IServerboundPacketObserver
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
