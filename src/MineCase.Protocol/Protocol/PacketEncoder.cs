using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Serialization;

namespace MineCase.Protocol
{
    public class PacketEncoder
    {
        private PacketDirection _direction;

        private PacketInfo _packetInfo;

        public PacketEncoder(PacketDirection direction, PacketInfo packetInfo)
        {
            _direction = direction;
            _packetInfo = packetInfo;
        }

        public void Encode(ISerializablePacket packet, Stream stream)
        {
            var packetId = _packetInfo.GetPacketId(packet);
            if (packetId < 0)
                throw new ArgumentException("Can not find packet id in PacketInfo.");

            using (BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                bw.WriteAsVarInt(packetId, out _);
                packet.Serialize(bw);
            }
        }
    }
}
