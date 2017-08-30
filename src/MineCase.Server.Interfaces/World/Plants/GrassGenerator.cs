using MineCase.Server.World.Biomes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Plants
{
    public class GrassGenerator : PlantsGenerator
    {
        private int _grassPerChunk;

        public GrassGenerator(int grassPerChunk = 1)
        {
            _grassPerChunk = grassPerChunk;
        }

        public override void Generate(IWorld world, Biome biome, Random random, BlockPos pos)
        {
            var width = ChunkConstants.BlockEdgeWidthInSection;
            for (int grassNum = 0; grassNum < _grassPerChunk; ++grassNum)
            {
                int x = random.Next(width) + 8;
                int z = random.Next(width) + 8;
                int height = world.getHeight(BlockPos.Add(pos, x, 0, z)).getY() * 2;

                if (height > 0)
                {
                    int r = random.Next(height);
                    biome.GetRandomGrass(random).generate(world, random, BlockPos.Add(pos, x, r, z));
                }
            }
        }

        private void GenGrass(IWorld world, int height, BlockPos pos)
        {
            for (int i = 0; i < height; ++i)
            {
                world.SetBlockState();
            }
        }
    }
}
