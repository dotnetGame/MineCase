using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Generation;
using MineCase.Server.World.Plants;
using Orleans;

namespace MineCase.Server.World.Biomes
{
    public class BiomeOcean : Biome
    {
        public BiomeOcean(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "ocean";
            _biomeId = BiomeId.Ocean;
        }
    }
}