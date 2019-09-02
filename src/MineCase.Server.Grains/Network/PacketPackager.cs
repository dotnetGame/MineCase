using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IO;
using MineCase.Protocol;

namespace MineCase.Server.Network
{
    internal class PacketPackager : IPacketPackager
    {
        public PacketPackager()
        {
        }

        public Task<(uint packetId, byte[] data)> PreparePacket(ISerializablePacket packet)
        {
            using (var stream = new MemoryStream())
            {
                using (var bw = new BinaryWriter(stream, Encoding.UTF8, true))
                    packet.Serialize(bw);
                return Task.FromResult((GetPacketId(packet), stream.ToArray()));
            }
        }

        private uint GetPacketId(ISerializablePacket packet)
        {
            var typeInfo = packet.GetType().GetTypeInfo();
            var attr = typeInfo.GetCustomAttribute<PacketAttribute>();
            return attr.PacketId;
        }
    }
}
