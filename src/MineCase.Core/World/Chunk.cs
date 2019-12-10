using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World;

namespace MineCase.Core.World
{
    public class Chunk
    {
        public bool Populated { get; set; }

        public bool Generated { get; set; }

        public Dictionary<Heightmaps.Type, Heightmap> HeightMaps { get; set; }

        public ChunkWorldPos Posistion { get; set; }

        public ChunkSection[] Sections { get; set; } = new ChunkSection[16];

        public Chunk()
        {
            Populated = false;
            Generated = false;
            HeightMaps = new Dictionary<Heightmaps.Type, Heightmap>();
        }
    }
}
