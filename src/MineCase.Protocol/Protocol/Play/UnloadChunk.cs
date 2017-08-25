using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x1D)]
    public sealed class UnloadChunk : ISerializablePacket
    {
        [SerializeAs(DataType.Int)]
        public int ChunkX;

        [SerializeAs(DataType.Int)]
        public int ChunkZ;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(ChunkX);
            bw.WriteAsInt(ChunkZ);
        }
    }
}
