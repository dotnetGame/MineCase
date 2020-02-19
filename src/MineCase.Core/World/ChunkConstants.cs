using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World
{
    public static class ChunkConstants
    {
        public const int ChunkHeight = 256;
        public const int SectionsPerChunk = 16;

        public const int BlockEdgeWidthInSection = 16;

        public const int BlocksInSection = BlockEdgeWidthInSection * BlockEdgeWidthInSection * BlockEdgeWidthInSection;

        public const int BlocksInChunk = BlocksInSection * SectionsPerChunk;
    }
}
