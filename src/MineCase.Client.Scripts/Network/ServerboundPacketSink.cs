using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Client.Network
{
    internal class ServerboundPacketSink : IPacketSink
    {
        private readonly IPacketPackager _packetPackager;

        public ServerboundPacketSink(IPacketPackager packetPackager)
        {
            _packetPackager = packetPackager;
        }

        public async Task SendPacket(ISerializablePacket packet)
        {
            var prepared = _packetPackager.PreparePacket(packet);
            await SendPacket(prepared.packetId, prepared.data);
        }

        public Task SendPacket(uint packetId, byte[] data)
        {
            var packet = new UncompressedPacket
            {
                PacketId = packetId,
                Data = new ArraySegment<byte>(data)
            };

            return Task.CompletedTask;
        }
    }
}
