using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Mine
{
    [StatelessWorker]
    public class MinableGeneratorGrain : Grain, IMinableGenerator
    {
        public async Task Generate(IWorld world, ChunkWorldPos chunkWorldPos, BlockState blockState, int count, int size, int minHeight, int maxHeight)
        {
            int seed = await world.GetSeed();

            BlockWorldPos curChunkCorner = chunkWorldPos.ToBlockWorldPos();

            int chunkSeed = chunkWorldPos.X ^ chunkWorldPos.Z ^ seed ^ blockState.GetHashCode();
            Random random = new Random(chunkSeed);

            if (minHeight > maxHeight)
            {
                int tmp = minHeight;
                minHeight = maxHeight;
                maxHeight = tmp;
            }
            else if (maxHeight == minHeight)
            {
                if (minHeight < 255)
                    ++maxHeight;
                else
                    --minHeight;
            }

            for (int j = 0; j < count; ++j)
            {
                BlockWorldPos blockpos = BlockWorldPos.Add(
                    curChunkCorner,
                    random.Next(16),
                    random.Next(maxHeight - minHeight) + minHeight,
                    random.Next(16));
                await GenerateSingle(world, chunkWorldPos, blockpos, blockState, size);
            }
        }

        public async Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState state, int size)
        {
            int seed = await world.GetSeed();

            int oreSeed = pos.X ^ pos.Z ^ seed ^ state.GetHashCode();
            Random random = new Random(oreSeed);

            await GenerateImpl(world, chunkWorldPos, pos, state, size, random);
        }

        private async Task GenerateImpl(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState state, int size, Random rand)
        {
            // 在xz平面上的方向
            float angle = (float)rand.NextDouble() * (float)Math.PI;

            // 起始点和结束点
            double startX = (double)((float)(pos.X + 8) + Math.Sin(angle) * (float)size / 8.0F);
            double endX = (double)((float)(pos.X + 8) - Math.Sin(angle) * (float)size / 8.0F);
            double startZ = (double)((float)(pos.Z + 8) + Math.Cos(angle) * (float)size / 8.0F);
            double endZ = (double)((float)(pos.Z + 8) - Math.Cos(angle) * (float)size / 8.0F);
            double startY = (double)(pos.Y + rand.Next(3) - 2);
            double endY = (double)(pos.Y + rand.Next(3) - 2);

            for (int i = 0; i < size; ++i)
            {
                // 插值参数
                float t = (float)i / (float)size;

                // 椭球中心
                double centerX = startX + (endX - startX) * (double)t;
                double centerY = startY + (endY - startY) * (double)t;
                double centerZ = startZ + (endZ - startZ) * (double)t;

                // 椭球尺寸（可以看出XZ和Y尺寸一样，应该是球）
                double scale = rand.NextDouble() * (double)size / 16.0D;
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
                                        if (blockpos.Y >= 0 &&
                                            blockpos.Y < 255 &&
                                            (await world.GetBlockState(this.GrainFactory, blockpos)) == BlockStates.Stone())
                                        {
                                            // 替换为矿石
                                            await world.SetBlockState(this.GrainFactory, blockpos, state);
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
