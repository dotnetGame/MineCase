using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Biome
{
    public class BiomeArray
    {
        private readonly Biome[] _biomes;

        public BiomeArray(Biome[] biomes)
        {
            _biomes = biomes;
        }

        public BiomeArray()
        {
            _biomes = new Biome[ChunkBiomeNum];
        }
    }
}
