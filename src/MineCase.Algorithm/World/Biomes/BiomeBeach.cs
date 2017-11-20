using System;
using System.Collections.Generic;
using System.Text;
using MineCase.World.Biomes;
using MineCase.World.Generation;

namespace MineCase.Algorithm.World.Biomes
{
    public class BiomeBeach : Biome
    {
        public BiomeBeach(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "beach";
            _biomeId = BiomeId.Beach;

            _baseHeight = 0.0F;
            _heightVariation = 0.025F;
            _temperature = 0.8F;
            _rainfall = 0.4F;

            _topBlock = BlockStates.Sand();
            _fillerBlock = BlockStates.Sand();

            _treesPerChunk = -999;
            _deadBushPerChunk = 0;
            _reedsPerChunk = 0;
            _cactiPerChunk = 0;
        }
    }
}
