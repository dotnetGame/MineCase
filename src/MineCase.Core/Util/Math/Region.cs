using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Math
{
    public struct Region
    {
        public ChunkPos BasePos { get; set; }
        public int XChunkNum { get; set; }
        public int ZChunkNum { get; set; }

        public Region(ChunkPos chunkPos, int x, int z)
        {
            BasePos = chunkPos;
            XChunkNum = x;
            ZChunkNum = z;
        }
    }
}
