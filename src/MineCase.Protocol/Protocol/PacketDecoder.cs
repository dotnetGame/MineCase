using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Serialization;

namespace MineCase.Protocol.Protocol
{
    public class PacketDecoder
    {
        private PacketDirection _direction;
        private PacketInfo _packetInfo;

        public PacketDecoder(PacketDirection direction, PacketInfo packetInfo)
        {
            _direction = direction;
            _packetInfo = packetInfo;
        }

        public ISerializablePacket Decode(Stream stream)
        {
            var br = new BinaryReader(stream);
            uint id = br.ReadAsVarInt(out _);
            var packet = _packetInfo.GetPacket(_direction, (int)id);
            packet.Deserialize(br);
            return packet;
        }
    }
}
