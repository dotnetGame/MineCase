using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Protocol.Status.Client
{
    [Packet(0x01, ProtocolType.Status, PacketDirection.ClientBound)]
    public sealed class Pong : ISerializablePacket
    {
        [SerializeAs(DataType.Long)]
        public long Payload;

        public void Deserialize(BinaryReader br)
        {
            Payload = br.ReadAsLong();
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsLong(Payload);
        }
    }
}
