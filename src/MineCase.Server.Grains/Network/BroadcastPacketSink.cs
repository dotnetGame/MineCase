using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using Orleans.Concurrency;

namespace MineCase.Server.Network
{
    internal class BroadcastPacketSink : IPacketSink
    {
        private IReadOnlyCollection<IPacketSink> _sinks;
        private readonly IPacketPackager _packetPackager;

        public BroadcastPacketSink(IReadOnlyCollection<IPacketSink> sinks, IPacketPackager packetPackager)
        {
            _sinks = sinks ?? Array.Empty<IPacketSink>();
            _packetPackager = packetPackager;
        }

        public async Task SendPacket(ISerializablePacket packet)
        {
            if (_sinks.Any())
            {
                var preparedPacket = await _packetPackager.PreparePacket(packet);
                await SendPacket(preparedPacket.PacketId, preparedPacket.Data.AsImmutable());
            }
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data)
        {
            return Task.WhenAll(from sink in _sinks
                                select sink.SendPacket(packetId, data));
        }

        public async Task SendPacket(ISerializablePacket packet, IPacketSink except)
        {
            if (_sinks.Any())
            {
                var preparedPacket = await _packetPackager.PreparePacket(packet);
                await SendPacket(preparedPacket.PacketId, preparedPacket.Data.AsImmutable(), except);
            }
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data, IPacketSink except)
        {
            return Task.WhenAll(from sink in _sinks
                                where !sink.Equals(except)
                                select sink.SendPacket(packetId, data));
        }
    }
}
