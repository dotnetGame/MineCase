using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Serialization;

namespace MineCase.Protocol
{
    public static class PacketDeserializer
    {
        public static T Deserialize<T>(ref SpanReader br)
            where T : IPacket, new()
        {
            var packet = new T();
            packet.Deserialize(ref br);
            return packet;
        }
    }
}
