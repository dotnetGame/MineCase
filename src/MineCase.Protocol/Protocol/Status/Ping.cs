using MineCase.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Status
{
    [Packet(0x01)]
    public sealed class Ping
    {
        [SerializeAs(DataType.Long)]
        public long Payload;

        public static Ping Deserialize(BinaryReader br)
        {
            return new Ping
            {
                Payload = br.ReadAsLong(),
            };
        }
    }

    [Packet(0x01)]
    public sealed class Pong : ISerializablePacket
    {
        [SerializeAs(DataType.Long)]
        public long Payload;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsLong(Payload);
        }
    }
}
