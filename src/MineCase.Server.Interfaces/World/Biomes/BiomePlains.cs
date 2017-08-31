using System;
using System.Collections.Generic;
using System.Text;
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
        public override void Decorate(IWorld world, Random rand, BlockPos pos)
        {
            float d0 = (_grassColorNoise.Noise((pos.X + 8) / 200.0F, 0.0F, (pos.Z + 8) / 200.0F) - 0.5F) * 2;

            if (d0 < -0.8F)
            {
                _grassGenerator.GrassPerChunk = 5;
            }
            else
            {
                _grassGenerator.GrassPerChunk = 10;

                for (int i = 0; i < 7; ++i)
                {
                    int x = rand.Next(16) + 8;
                    int z = rand.Next(16) + 8;
                    int y = rand.Next(world.getHeight(BlockPos.Add(pos, x, 0, z)).getY() + 32);
                    _grassGenerator.Generate(world, rand, BlockPos.Add(pos, x, y, z));
                }
            }
        }

        // 使用花装点
        private void GenFlowers()
        {

        }
    }
}