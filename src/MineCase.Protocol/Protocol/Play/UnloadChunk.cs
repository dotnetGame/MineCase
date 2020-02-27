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
    [Packet(0x1E)]
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
