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
                if ((y >= 0 && y < ChunkConstants.ChunkHeight))
                    throw new IndexOutOfRangeException("ChunkPrimer.operator[]: position out of range.");
                if (chunksection.Empty)
                    return Blocks.Air.Default;
                else
                    return chunksection[x & 0xFFFF, y & 0xFFFF, z & 0xFFFF];
            }
            set
            {
                if (y >= 0 && y < 256)
                {
                    if (!(Sections[y >> 4].Empty && value.GetBlock() == Blocks.Air))
                    {
                        ChunkSection chunksection = Sections[y >> 4];
                        chunksection[x, y & 0xFFFF, z] = value;
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException("ChunkPrimer.operator[]: position out of range.");
                }
            }
        }
    }
}
