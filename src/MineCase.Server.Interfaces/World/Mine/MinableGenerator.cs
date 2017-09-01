using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Mine
{
    public class MinableGenerator
    {
        public BlockState OreBlock { get; set; }

        public int NumberOfBlocks { get; set; } // 每一坨矿物的数量

        public int MaxHeight { get; set; }

        public int MinHeight { get; set; }

        public MinableGenerator(BlockState state, int blockCount)
        {
            OreBlock = state;
            NumberOfBlocks = blockCount;
        }

        public void Generate(IWorld world, ChunkColumnStorage chunk, Random random, BlockPos position, int count)
        {
            if (MinHeight > MaxHeight)
            {
                int tmp = MinHeight;
                MinHeight = MaxHeight;
                MaxHeight = tmp;
            }
            else if (MaxHeight == MinHeight)
            {
                if (MinHeight < 255)
                    ++MaxHeight;
                else
                    --MinHeight;
            }

            for (int j = 0; j < count; ++j)
            {
                BlockPos blockpos = BlockPos.Add(
                    position,
                    random.Next(16),
                    random.Next(MaxHeight - MinHeight) + MinHeight,
                    random.Next(16));
                OreGenerate(world, chunk, random, position);
            }
        }

        private void OreGenerate(IWorld world, ChunkColumnStorage chunk, Random rand, BlockPos position)
        {

        }
    }
}
