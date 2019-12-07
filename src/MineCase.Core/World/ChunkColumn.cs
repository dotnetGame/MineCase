using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World;

namespace MineCase.Core.World
{
    public class ChunkColumn
    {
        public bool Populated { get; set; } = false;

        public bool Generated { get; set; } = false;

        public int[,] GroundHeight { get; set; } = new int[16, 16];

        public ChunkColumnCompactStorage Storage { get; set; }
    }
}
