using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.Biomes;

namespace MineCase.Server.World.Plants
{
    public class GrassGenerator : PlantsGenerator
    {
        public int GrassPerChunk { get; set; } = 1;

        public GrassGenerator(int grassPerChunk = 1)
        {
            GrassPerChunk = grassPerChunk;
        }

        public override void Generate(IWorld world, IChunkColumn chunk, Biome biome, Random random, BlockPos pos)
        {
            var width = ChunkConstants.BlockEdgeWidthInSection;
            for (int grassNum = 0; grassNum < _grassPerChunk; ++grassNum)
            {
                
            }

            for (BlockState iblockstate = BlockAccessor.GetBlockState(pos.X, pos.Y, pos.Z);
                (iblockstate == BlockStates.Air() || iblockstate != BlockStates.Leaves()) && pos.Y > 0;
                iblockstate = BlockAccessor.GetBlockState(pos.X, pos.Y, pos.Z))
            {
                pos.Y = pos.Y - 1;
            }

            for (int i = 0; i < 128; ++i)
            {
                BlockPos blockpos = BlockPos.Add(pos, random.Next(8) - random.Next(8), random.Next(4) - random.Next(4), random.Next(8) - random.Next(8));
                if (worldIn.isAirBlock(blockpos) && Blocks.TALLGRASS.CanBlockStay(worldIn, blockpos, this.tallGrassState))
                {
                    worldIn.setBlockState(blockpos, this.tallGrassState, 2);
                }
            }

            return true;
        }

        private void GenGrass(IWorld world, IChunkColumn chunk, BlockPos pos)
        {
        }
    }
}
