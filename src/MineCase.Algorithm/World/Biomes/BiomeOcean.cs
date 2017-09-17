using MineCase.World.Biomes;
using MineCase.World.Generation;

namespace MineCase.Algorithm.World.Biomes
{
    public class BiomeOcean : Biome
    {
        public BiomeOcean(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "ocean";
            _biomeId = BiomeId.Ocean;
            _baseHeight = -1.0F;
            _heightVariation = 0.1F;
        }
    }
}