using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Protocol.Protocol;
using MineCase.Serialization;

namespace MineCase.Protocol.Protocol.Status.Server
{
    [Packet(0x01, ProtocolType.Status, PacketDirection.ServerBound)]
    public sealed class Ping : ISerializablePacket
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
