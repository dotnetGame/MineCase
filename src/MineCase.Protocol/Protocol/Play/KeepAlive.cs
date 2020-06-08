using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
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
