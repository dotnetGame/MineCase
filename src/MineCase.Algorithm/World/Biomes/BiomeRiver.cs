using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World.Biomes;
using MineCase.World.Generation;

namespace MineCase.Algorithm.World.Biomes
{
    public class BiomeRiver : Biome
    {
        public BiomeRiver(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "river";
            _biomeId = BiomeId.River;

            _baseHeight = -0.5F;
            _heightVariation = 0.0F;
        }
    }
}
