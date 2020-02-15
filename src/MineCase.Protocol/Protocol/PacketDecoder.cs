using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Protocol.Protocol
{
    public class PacketDecoder
    {
        private PacketDirection _direction;

        public PacketDecoder(PacketDirection direction)
        {
            _direction = direction;
        }

        public ISerializablePacket DecodeAsync(Stream stream)
        {

        }
    }
}
