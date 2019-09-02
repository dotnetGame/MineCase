using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using Orleans.Concurrency;

namespace MineCase.Gateway.Network
{
    public class ClientboundPacketSink
    {
        private PacketPackager _packetPackager;

        public ClientboundPacketSink(PacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        public async Task SendPacket(ISerializablePacket packet)
        {
            var prepared = await _packetPackager.PreparePacket(packet);
            await SendPacket(prepared.packetId, prepared.data.AsImmutable());
        }

        public Task SendPacket(uint packetId, Immutable<byte[]> data)
        {
            var packet = new UncompressedPacket
            {
                PacketId = packetId,
                Data = new byte[data.Value.Length]
            };
            return Task.CompletedTask;
        }
    }
}
