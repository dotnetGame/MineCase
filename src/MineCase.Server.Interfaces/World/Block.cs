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

        public void SetBlockState(BlockState state)
        {
            Id=state.Id;
            MetaValue=state.MetaValue;
        }

        public void SetBlockId(BlockId id)
        {
            Id=(uint)id;
        }
    }

    public enum BlockId : uint
    {
        Air=0,
        Stone=1,
        Grass=2,
        Dirt=3,
        Cobblestone=4,
        Water=9
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
            Blocks[GetBlockIndex(x, y, z)].SetBlockState(state);
        }

        public void SetBlockId(int x, int y, int z, BlockId id)
        {
            Blocks[GetBlockIndex(x, y, z)].SetBlockId(id);
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
            Sections[GetSectionIndex(x, y, z)].SetBlockState(x, y >> 4, z, state);
        }

        public void SetBlockId(int x, int y, int z, BlockId id)
        {
            Sections[GetSectionIndex(x, y, z)].SetBlockId(x, y >> 4, z, id);
        }

        public void GenerateSkylightMap()
        {
        }
    }
}
