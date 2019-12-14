using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Block;
using MineCase.World.Chunk;
using MineCase.Serialization;

namespace MineCase.World.Chunk
{
    public class ChunkSection
    {
        public static ChunkSection EmptySection = null;

        public BlockStateContainer<BlockState> Data { get; set; }

        public int BlockCount { get; set; }

        public bool IsEmpty()
        {
            return Data.Length == 0;
        }

        public int GetSize()
        {
            return 2 + Data.GetSerializedSize();
        }
    }
    public static class BinaryWriterExtensions
    {
        public static void WriteAsChunkSection(this BinaryWriter bw, ChunkSection value)
        {
            bw.WriteAsShort((short)value.BlockCount);
            bw.WriteAsBlockStateContainer(value.Data);
        }
    }
}
