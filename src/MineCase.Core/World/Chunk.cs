using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World
{
    public class ChunkColumn
    {
        public bool Populated { get; set; }

        public bool Generated { get; set; }

        public Biome.Biome[] BlockBiomeArray { get; set; }

        public Dictionary<Heightmaps.Type, Heightmap> HeightMaps { get; set; }

        public Dictionary<BlockWorldPos, BlockEntity.BlockEntity> BlockEntities { get; set; }

        public ChunkWorldPos Posistion { get; set; }

        public ChunkSection[] Sections { get; set; } = new ChunkSection[16];

        public ChunkColumn()
        {
            Populated = false;
            Generated = false;
            HeightMaps = new Dictionary<Heightmaps.Type, Heightmap>();
        }
    }
}
