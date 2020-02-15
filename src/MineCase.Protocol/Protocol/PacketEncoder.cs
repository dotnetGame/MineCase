using MineCase.Protocol.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Protocol.Protocol
{
    public class PacketEncoder
    {
        private PacketDirection _direction;

        private PacketInfo _info;

        public PacketEncoder(PacketDirection direction)
        {
            _direction = direction;
            _info = new PacketInfo();
        }

        public void EncodeAsync(ISerializablePacket packet, Stream stream)
        {
            var packetId = _info.GetPacketId(packet);
            if (packetId < 0)
                throw new ArgumentException("Can not find packet id in PacketInfo.");
            BinaryWriter bw = new BinaryWriter(stream);
            bw.WriteAsVarInt((uint)packetId, out _);
            packet.Serialize(bw);
        }
    }
}
