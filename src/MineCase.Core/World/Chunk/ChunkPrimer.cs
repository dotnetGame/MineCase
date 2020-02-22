using MineCase.Block;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World.Chunk
{
    public class ChunkPrimer
    {
        public ChunkSection[] Sections { get; } = new ChunkSection[ChunkConstants.BlockEdgeWidthInSection];

        public BlockState this[int x, int y, int z]
        {
            get
            {
                ChunkSection chunksection = Sections[y >> 4];
                return chunksection.Empty ? Blocks.AIR.getDefaultState() : chunksection[x & 15, y & 15, z & 15];
            }
            set => _data[GetIndex(x, y, z)] = value;
        }
    }
}
