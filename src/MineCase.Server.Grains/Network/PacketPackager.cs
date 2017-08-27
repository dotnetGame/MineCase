using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;

namespace MineCase.Server.Network
{
    internal class PacketPackager : IPacketPackager
    {
        public Task<(uint packetId, byte[] data)> PreparePacket(ISerializablePacket packet)
        {
            using (var stream = new MemoryStream())
            using (var bw = new BinaryWriter(stream))
            {
                packet.Serialize(bw);
                bw.Flush();
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
