using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Generation;
using MineCase.Server.World.Plants;
using Orleans;

namespace MineCase.Server.World.Biomes
{
    public class BiomePlains : Biome
    {
        private GrassGenerator _grassGenerator;

        private FlowersGenerator _flowersGenerator;

        public BiomePlains(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _grassGenerator = new GrassGenerator();
            _flowersGenerator = new FlowersGenerator();
        }

        // 添加其他东西
        public override async Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            float d0 = (_grassColorNoise.Noise((pos.X + 8) / 200.0F, 0.0F, (pos.Z + 8) / 200.0F) - 0.5F) * 2;

            if (d0 < -0.8F)
            {
                _flowersGenerator.FlowersPerChunk = 15;
                _grassGenerator.GrassPerChunk = 5 * 7;
            }
            else
            {
                _flowersGenerator.FlowersPerChunk = 4;
                _grassGenerator.GrassPerChunk = 10 * 7;
            }

            await _grassGenerator.Generate(world, grainFactory, chunk, this, rand, pos);
            await _flowersGenerator.Generate(world, grainFactory, chunk, this, rand, pos);
            await base.Decorate(world, grainFactory, chunk, rand, pos);
        }
    }
}