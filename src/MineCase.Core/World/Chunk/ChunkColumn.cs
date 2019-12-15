using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World.Biome;
using MineCase.World.Generation;

namespace MineCase.World.Chunk
{
    public class ChunkColumn
    {
        public Biome.Biome[] BlockBiomeArray { get; set; }

        public Dictionary<Heightmaps.Type, Heightmap> HeightMaps { get; set; }

        public Dictionary<BlockWorldPos, BlockEntity.BlockEntity> BlockEntities { get; set; }

        public ChunkWorldPos Posistion { get; set; }

        public ChunkSection[] Sections { get; set; }

        public ChunkColumn()
        {
            BlockBiomeArray = new Biome.Biome[ChunkConstants.BlockEdgeWidthInSection * ChunkConstants.BlockEdgeWidthInSection];
            for (int i = 0; i <BlockBiomeArray.Length; ++i)
            {
                BlockBiomeArray[i] = new Biome.BiomePlains(new BiomeProperties { }, new GeneratorSettings());
            }
            HeightMaps = new Dictionary<Heightmaps.Type, Heightmap>();
            BlockEntities = new Dictionary<BlockWorldPos, BlockEntity.BlockEntity>();
            Posistion = new ChunkWorldPos { X = 0, Z = 0 };
            Sections = new ChunkSection[ChunkConstants.BlockEdgeWidthInSection];
        }
    }
}
