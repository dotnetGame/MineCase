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
    }

    public sealed class ChunkSection
    {
        public byte BitsPerBlock { get; set; }

        public Block[] Blocks { get; set; }
    }

    public sealed class ChunkColumn
    {
        public ChunkSection[] Sections { get; set; }

        public uint SectionBitMask { get; set; }

        public byte[] Biomes { get; set; }
    }
}
