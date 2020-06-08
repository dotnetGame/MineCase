using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x41)]
    public sealed class UpdateViewPosition : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public int ChunkX;

        [SerializeAs(DataType.VarInt)]
        public int ChunkZ;

        public static UpdateViewPosition Deserialize(ref SpanReader br)
        {
            return new UpdateViewPosition
            {
                ChunkX = (int)br.ReadAsVarInt(out _),
                ChunkZ = (int)br.ReadAsVarInt(out _),
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt((uint)ChunkX, out _);
            bw.WriteAsVarInt((uint)ChunkZ, out _);
        }
    }
}
