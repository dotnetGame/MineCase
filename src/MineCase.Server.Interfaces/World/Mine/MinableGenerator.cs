using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.World.Mine
{
    public class MinableGenerator
    {
        public BlockState OreBlock { get; set; }

        public int NumberOfBlocks { get; set; } // 每一坨矿物的数量

        public int MaxHeight { get; set; }

        public int MinHeight { get; set; }

        public MinableGenerator(BlockState state, int blockCount, int maxHeight, int minHeight)
        {
            OreBlock = state;
            NumberOfBlocks = blockCount;
            MaxHeight = maxHeight;
            MinHeight = minHeight;
        }

        public void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos position, int count)
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
                BlockWorldPos blockpos = BlockWorldPos.Add(
                    position,
                    random.Next(16),
                    random.Next(MaxHeight - MinHeight) + MinHeight,
                    random.Next(16));
                OreGenerate(world, grainFactory, chunk, random, blockpos);
            }
        }

        private void OreGenerate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos position)
        {
            // 在xz平面上的方向
            float angle = (float)rand.NextDouble() * (float)Math.PI;

            // 起始点和结束点
            double startX = (double)((float)(position.X + 8) + Math.Sin(angle) * (float)NumberOfBlocks / 8.0F);
            double endX = (double)((float)(position.X + 8) - Math.Sin(angle) * (float)NumberOfBlocks / 8.0F);
            double startZ = (double)((float)(position.Z + 8) + Math.Cos(angle) * (float)NumberOfBlocks / 8.0F);
            double endZ = (double)((float)(position.Z + 8) - Math.Cos(angle) * (float)NumberOfBlocks / 8.0F);
            double startY = (double)(position.Y + rand.Next(3) - 2);
            double endY = (double)(position.Y + rand.Next(3) - 2);

            for (int i = 0; i < NumberOfBlocks; ++i)
            {
                // 插值参数
                float t = (float)i / (float)NumberOfBlocks;

                // 椭球中心
                double centerX = startX + (endX - startX) * (double)t;
                double centerY = startY + (endY - startY) * (double)t;
                double centerZ = startZ + (endZ - startZ) * (double)t;

                // 椭球尺寸（可以看出XZ和Y尺寸一样，应该是球）
                double scale = rand.NextDouble() * (double)NumberOfBlocks / 16.0D;
                double diameterXZ = (double)(Math.Sin((float)Math.PI * t) + 1.0F) * scale + 1.0D;
                double diameterY = (double)(Math.Sin((float)Math.PI * t) + 1.0F) * scale + 1.0D;

                // 椭球包围盒
                int minX = (int)Math.Floor(centerX - diameterXZ / 2.0D);
                int minY = (int)Math.Floor(centerY - diameterY / 2.0D);
                int minZ = (int)Math.Floor(centerZ - diameterXZ / 2.0D);
                int maxX = (int)Math.Floor(centerX + diameterXZ / 2.0D);
                int maxY = (int)Math.Floor(centerY + diameterY / 2.0D);
                int maxZ = (int)Math.Floor(centerZ + diameterXZ / 2.0D);

                // 把这个椭球里的方块替换为矿石
                for (int x = minX; x <= maxX; ++x)
                {
                    double xDist = ((double)x + 0.5D - centerX) / (diameterXZ / 2.0D);

                    // 参考椭球方程
                    if (xDist * xDist < 1.0D)
                    {
                        for (int y = minY; y <= maxY; ++y)
                        {
                            double yDist = ((double)y + 0.5D - centerY) / (diameterY / 2.0D);

                            // 参考椭球方程
                            if (xDist * xDist + yDist * yDist < 1.0D)
                            {
                                for (int z = minZ; z <= maxZ; ++z)
                                {
                                    double zDist = ((double)z + 0.5D - centerZ) / (diameterXZ / 2.0D);

                                    // 参考椭球方程
                                    if (xDist * xDist + yDist * yDist + zDist * zDist < 1.0D)
                                    {
                                        BlockWorldPos blockpos = new BlockWorldPos(x, y, z);
                                        BlockChunkPos posInChunk = blockpos.ToBlockChunkPos();
                                        if (posInChunk.Y >= 0 &&
                                            posInChunk.Y < 255 &&
                                            chunk[posInChunk.X, posInChunk.Y, posInChunk.Z] == BlockStates.Stone())
                                        {
                                            // 替换为矿石
                                            chunk[posInChunk.X, posInChunk.Y, posInChunk.Z] = OreBlock;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
