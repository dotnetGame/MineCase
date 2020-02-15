using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Protocol.Protocol;
using MineCase.Protocol.Serialization;
using MineCase.Serialization;

namespace MineCase.Protocol.Status
{
    [Packet(0x01)]
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

    [Packet(0x01)]
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
