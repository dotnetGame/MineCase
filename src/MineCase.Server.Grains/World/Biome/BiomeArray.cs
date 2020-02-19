using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World.Biome;

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
            _biomes = new Biome[BiomeConstants.ChunkBiomeNum];
        }
    }
}
