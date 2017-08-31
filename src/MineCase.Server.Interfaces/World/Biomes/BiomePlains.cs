using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Plants;

namespace MineCase.Server.World.Biomes
{
    public class BiomePlains : Biome
    {
        private GrassGenerator _grassGenerator;

        public BiomePlains(BiomeProperties properties)
            : base(properties)
        {
            _grassGenerator = new GrassGenerator();
        }

        // 添加其他东西
        public override async Task Decorate(IWorld world, ChunkColumnStorage chunk, Random rand, BlockPos pos)
        {
            float d0 = (_grassColorNoise.Noise((pos.X + 8) / 200.0F, 0.0F, (pos.Z + 8) / 200.0F) - 0.5F) * 2;

            if (d0 < -0.8F)
            {
                _grassGenerator.GrassPerChunk = 5;
            }
            else
            {
                _grassGenerator.GrassPerChunk = 10;
            }

            await _grassGenerator.Generate(world, chunk, this, rand, pos);
        }
    }
}