using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World;

namespace MineCase.Core.World
{
    public class ChunkColumn
    {
        public bool Populated { get; set; }

        public bool Generated { get; set; }

        public int[,] GroundHeight { get; set; }

        public ChunkColumnCompactStorage Storage { get; set; }

        public ChunkColumn()
        {
            Populated = false;
            Generated = false;
            GroundHeight = new int[16, 16];
            Storage = new ChunkColumnCompactStorage();
        }
    }
}
