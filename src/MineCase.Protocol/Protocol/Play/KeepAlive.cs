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
    [Packet(0x0C)]
    public sealed class ServerboundKeepAlive
    {
        [SerializeAs(DataType.VarInt)]
        public uint KeepAliveId;

        public static ServerboundKeepAlive Deserialize(ref SpanReader br)
        {
            return new ServerboundKeepAlive
            {
                KeepAliveId = br.ReadAsVarInt(out _)
            };
        }
    }

#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x1F)]
    public sealed class ClientboundKeepAlive : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint KeepAliveId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(KeepAliveId, out _);
        }
    }
}
