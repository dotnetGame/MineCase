using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // FIXME: 1.15.2 no longer has this packet
    [Packet(0x25)]
    public sealed partial class UpdateLight : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public int ChunkX;

        [SerializeAs(DataType.VarInt)]
        public int ChunkZ;

        [SerializeAs(DataType.VarInt)]
        public int SkyLightMask;

        [SerializeAs(DataType.VarInt)]
        public int BlockLightMask;

        [SerializeAs(DataType.VarInt)]
        public int EmptySkyLightMask;

        [SerializeAs(DataType.VarInt)]
        public int EmptyBlockLightMask;

        [SerializeAs(DataType.VarIntArray)]
        public int[] SkyLightArrays;

        [SerializeAs(DataType.VarIntArray)]
        public int[] BlockLightArrays;

        public void Deserialize(ref SpanReader br)
        {
            ChunkX = (int)br.ReadAsVarInt(out _);
            ChunkZ = (int)br.ReadAsVarInt(out _);
            SkyLightMask = (int)br.ReadAsVarInt(out _);
            BlockLightMask = (int)br.ReadAsVarInt(out _);
            EmptySkyLightMask = (int)br.ReadAsVarInt(out _);
            EmptyBlockLightMask = (int)br.ReadAsVarInt(out _);
        }

        public void Serialize(BinaryWriter bw)
        {
            throw new NotImplementedException();
        }
    }
}
