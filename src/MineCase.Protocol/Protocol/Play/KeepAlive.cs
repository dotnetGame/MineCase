using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x0F)]
    public sealed class ServerboundKeepAlive
    {
        [SerializeAs(DataType.Long)]
        public long KeepAliveId;

        public static ServerboundKeepAlive Deserialize(ref SpanReader br)
        {
            return new ServerboundKeepAlive
            {
                KeepAliveId = br.ReadAsLong()
            };
        }
    }

#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x21)]
    public sealed class ClientboundKeepAlive : ISerializablePacket
    {
        [SerializeAs(DataType.Long)]
        public long KeepAliveId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsLong(KeepAliveId);
        }
    }
}
