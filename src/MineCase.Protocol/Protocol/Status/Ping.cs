using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Status
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x01)]
    public sealed class Ping : ISerializablePacket
    {
        [SerializeAs(DataType.Long)]
        public long Payload;

        public static Ping Deserialize(ref SpanReader br)
        {
            return new Ping
            {
                Payload = br.ReadAsLong(),
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsLong(Payload);
        }
    }

#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x01)]
    public sealed class Pong : ISerializablePacket
    {
        [SerializeAs(DataType.Long)]
        public long Payload;

        public static Pong Deserialize(ref SpanReader br)
        {
            return new Pong
            {
                Payload = br.ReadAsLong(),
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsLong(Payload);
        }
    }
}
