using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Game.Entities;
using MineCase.World.Biomes;
using MineCase.World.Generation;

namespace MineCase.Algorithm.World.Biomes
{
    public class BiomeSavanna : Biome
    {
        public BiomeSavanna(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "savanna";
            _biomeId = BiomeId.Savanna;
            _baseHeight = 0.125f;
            _heightVariation = 0.05f;
            _temperature = 1.2f;
            _rainfall = 0.0f;
            _enableSnow = false;
            _enableRain = false;
        }
    }
}
