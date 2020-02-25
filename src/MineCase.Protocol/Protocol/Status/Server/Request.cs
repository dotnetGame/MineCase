using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Status.Server
{
    [Packet(0x00, ProtocolType.Status, PacketDirection.ServerBound)]
    public sealed class Request : ISerializablePacket
    {
        public static readonly Request Empty = new Request();

        public void Deserialize(BinaryReader br)
        {
        }

        public void Serialize(BinaryWriter bw)
        {
        }
    }
}
