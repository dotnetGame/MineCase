using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World
{
    public sealed class Block
    {
        public uint Id { get; set; }

        public uint MetaValue { get; set; }

        public byte BlockLight { get; set; }

        public byte SkyLight { get; set; }

        public void setBlockState(BlockState state)
        {
            Id=state.Id;
            MetaValue=state.MetaValue;
        }
    }

    enum BlockId : uint
    {
        Air=0,
        Stone=1,
        Grass=2,
        Dirt=3,
        Cobblestone=4
    }

    public class BlockState
    {
        public uint Id { get; set; }

        public uint MetaValue { get; set; }
    }

    public sealed class ChunkSection
    {
        public byte BitsPerBlock { get; set; }

        public Block[] Blocks { get; set; }

        private static int GetBlockIndex(int x, int y, int z)
        {
            return x << 8 | z << 4 | y;
        }

        public void SetBlockState(int x, int y, int z, BlockState state)
        {
            Blocks[GetBlockIndex(x,y,z)].setBlockState(state);
        }
    }

    public sealed class ChunkColumn
    {
        private BlockState _defaultBlockState;
        public ChunkSection[] Sections { get; set; }

        public uint SectionBitMask { get; set; }

        public byte[] Biomes { get; set; }

        private static int GetSectionIndex(int x, int y, int z)
        {
            return y >> 4;
        }
        public void SetBlockState(int x, int y, int z, BlockState state)
        {
            Sections[GetSectionIndex(x, y, z)].SetBlockState(x,y >> 4,z,state);
        }
        public void GenerateSkylightMap()
        {
            
        }
    }
}
