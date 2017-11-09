using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public ClientSession(IPacketRouter packetRouter, IBufferPool<byte> bufferPool, ObjectPool<UncompressedPacket> uncompressedPacketObjectPool)
        {
            _tcpClient = new TcpClient();
            _bufferPool = bufferPool;
            _uncompressedPacketObjectPool = uncompressedPacketObjectPool;
            _packetRouter = packetRouter;
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            await _tcpClient.ConnectAsync(IPAddress.Loopback, 25565);
            using (_remoteStream = _tcpClient.GetStream())
            {
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
    }
}
