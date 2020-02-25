using MineCase.Block;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World.Chunk
{
    public class ChunkColumn
    {
        public static ChunkSection EmptySection = null;
        public ChunkSection[] Sections { get; set; } = new ChunkSection[ChunkConstants.BlockEdgeWidthInSection];

        public Biome.BiomeId[] Biomes { get; set; }

        public UInt16 SectionBitMask {
            get {
                UInt16 mask = 0;
                for (int i = 0; i < Sections.Length; ++i)
                {
                    ChunkSection chunksection = Sections[i];
                    if (chunksection != EmptySection)
                    {
                        mask |= (UInt16)(1 << i);
                    }
                }
                return mask;
            }
        }

        public bool FullChunk { get => SectionBitMask == 0xFFFF; }

        public BlockState this[int x, int y, int z]
        {
            get
            {
                ChunkSection chunksection = Sections[y >> 4];
                if ((y >= 0 && y < ChunkConstants.ChunkHeight))
                    throw new IndexOutOfRangeException("ChunkColumn.operator[]: position out of range.");
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
                    throw new IndexOutOfRangeException("ChunkColumn.operator[]: position out of range.");
                }
            }
        }
    }
}
