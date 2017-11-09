using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IO;
using MineCase.Protocol;

namespace MineCase.Client.Network
{
    internal class PacketPackager : IPacketPackager
    {
        private readonly RecyclableMemoryStreamManager _memoryStreamMgr;

        public PacketPackager(RecyclableMemoryStreamManager memoryStreamMgr)
        {
            _memoryStreamMgr = memoryStreamMgr;
        }

        public (uint packetId, byte[] data) PreparePacket(ISerializablePacket packet)
        {
            using (var stream = _memoryStreamMgr.GetStream())
            {
                using (var bw = new BinaryWriter(stream, Encoding.UTF8, true))
                    packet.Serialize(bw);
                return (GetPacketId(packet), stream.ToArray());
            }
        }

        private uint GetPacketId(ISerializablePacket packet)
        {
            var attr = packet.GetType().GetCustomAttribute<PacketAttribute>();
            return attr.PacketId;
        }
    }
}
