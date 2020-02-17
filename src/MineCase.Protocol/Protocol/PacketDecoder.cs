using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Serialization;

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

        public ISerializablePacket Decode(ProtocolType protocolType, Stream stream)
        {
            ISerializablePacket packet = null;
            using (BinaryReader br = new BinaryReader(stream, Encoding.UTF8, true))
            {
                int id = br.ReadAsVarInt(out _);

                // System.Console.WriteLine($"Read packet id: {id:X2}");
                packet = _packetInfo.GetPacket(_direction, protocolType, id);
                packet.Deserialize(br);
            }

            return packet;
        }
    }
}
