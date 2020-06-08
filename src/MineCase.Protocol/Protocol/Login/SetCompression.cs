using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x03)]
    public sealed class SetCompression : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Threshold;

        public static SetCompression Deserialize(ref SpanReader br)
        {
            return new SetCompression
            {
                Threshold = br.ReadAsVarInt(out _),
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(Threshold, out _);
        }
    }
}
