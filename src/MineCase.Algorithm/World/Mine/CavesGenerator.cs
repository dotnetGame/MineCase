using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.Biomes;
using MineCase.World;

namespace MineCase.Server.World.Mine
{
    public class CavesGenerator : MapGenerator
    {
        private static HashSet<uint> _canReplaceSet;

        static CavesGenerator()
        {
            _canReplaceSet = new HashSet<uint>();
            _canReplaceSet.Add((uint)BlockId.Stone);
            _canReplaceSet.Add((uint)BlockId.Dirt);
            _canReplaceSet.Add((uint)BlockId.GrassBlock);
            _canReplaceSet.Add((uint)BlockId.HardenedClay);
            _canReplaceSet.Add((uint)BlockId.StainedClay);
            _canReplaceSet.Add((uint)BlockId.Sandstone);
            _canReplaceSet.Add((uint)BlockId.RedSandstone);
            _canReplaceSet.Add((uint)BlockId.Mycelium);
            _canReplaceSet.Add((uint)BlockId.SnowLayer);
        }

        public CavesGenerator(MapGenerationInfo info, int range = 8)
            : base(info, range)
        {
        }

        protected override void RecursiveGenerate(MapGenerationInfo info, int chunkX, int chunkZ, int centerChunkX, int centerChunkZ, ChunkColumnStorage chunk, Biome biome)
        {
            // 之前根据chunkXZ设置种子了
            int seedPointCount = _rand.Next(15);

            // 仅1/7概率生成洞穴
            if (_rand.Next(7) != 0)
            {
                seedPointCount = 0;
            }

            for (int i = 0; i < seedPointCount; ++i)
            {
                // 在chunk内x=0-16,y=8~127,z=0-16随机选种子点
                double seedPointX = (double)(chunkX * 16 + _rand.Next(16));
                double seedPointY = (double)_rand.Next(120) + 8;
                double seedPointZ = (double)(chunkZ * 16 + _rand.Next(16));
                int directionCount = 1;

                // 四分之一概率挖一个默认的洞
                if (_rand.Next(4) == 0)
                {
                    // 使用默认参数挖出一个洞
                    AddTunnel( _rand.Next(), centerChunkX, centerChunkZ, chunk, biome, seedPointX, seedPointY, seedPointZ);
                    directionCount += _rand.Next(4);
                }

                // 向几个方向挖洞
                for (int j = 0; j < directionCount; ++j)
                {
                    float yawAngle = (float)_rand.NextDouble() * (float)Math.PI * 2.0F;
                    float pitchAngle = ((float)_rand.NextDouble() - 0.5F) * 2.0F / 8.0F;
                    float rangeScale = (float)_rand.NextDouble() * 2.0F + (float)_rand.NextDouble();

                    if (_rand.Next(10) == 0)
                    {
                        // 扩大到1~3倍
                        rangeScale *= (float)_rand.NextDouble() * 3.0F + 1.0F;
                    }

                    AddTunnel(_rand.Next(), centerChunkX, centerChunkZ, chunk, biome, seedPointX, seedPointY, seedPointZ, rangeScale, yawAngle, pitchAngle, 0, 0, 1.0D);
                }
            }
        }

        // 挖洞
        protected void AddTunnel(int seed, int centerChunkX, int centerChunkZ, ChunkColumnStorage chunk, Biome biome, double seedPointX, double seedPointY, double seedPointZ, float rangeScale, float yawAngle, float pitchAngle, int smallRange, int bigRange, double heightScale)
        {
            double centerBlockX = (double)(centerChunkX * 16 + 8);
            double centerBlockZ = (double)(centerChunkZ * 16 + 8);
            float yawAngleOffset = 0.0F;
            float pitchAngleOffset = 0.0F;
            Random random = new Random(seed);

            if (bigRange <= 0)
            {
                int tmp = (_range - 1) * 16;
                bigRange = tmp - random.Next(tmp / 4);
            }

            bool smallRangeIsNull = false;

            if (smallRange == -1)
            {
                smallRange = bigRange / 2;
                smallRangeIsNull = true;
            }

            // 可能的扩展点
            int keyPoint = random.Next(bigRange / 2) + bigRange / 4;

            bool flag = random.Next(6) == 0;

            // 循环挖出一条道路
            for (; smallRange < bigRange; ++smallRange)
            {
                // 用sin从1到0过渡
                double xzRange = 1.5D + (double)(Math.Sin((float)smallRange * (float)Math.PI / (float)bigRange) * rangeScale);
                double yRange = xzRange * heightScale;

                // 向yawAngle、pitchAngle方向偏移一个单位
                float cos = (float)Math.Cos(pitchAngle);
                float sin = (float)Math.Sin(pitchAngle);
                seedPointX += (double)(Math.Cos(yawAngle) * cos);
                seedPointY += (double)sin;
                seedPointZ += (double)(Math.Sin(yawAngle) * cos);

                // 1/6概率俯仰角衰减较慢
                if (flag)
                {
                    pitchAngle *= 0.92F;
                }
                else
                {
                    pitchAngle *= 0.7F;
                }

                pitchAngle += pitchAngleOffset * 0.1F;
                yawAngle += yawAngleOffset * 0.1F;
                pitchAngleOffset *= 0.9F;
                yawAngleOffset *= 0.75F;
                pitchAngleOffset += (float)((random.NextDouble() - random.NextDouble()) * random.NextDouble() * 2.0F);
                yawAngleOffset += (float)((random.NextDouble() - random.NextDouble()) * random.NextDouble() * 4.0F);

                if (!smallRangeIsNull
                    && smallRange == keyPoint
                    && rangeScale > 1.0F
                    && bigRange > 0)
                {
                    // 向左右两边扩展
                    AddTunnel(random.Next(), centerChunkX, centerChunkZ, chunk, biome, seedPointX, seedPointY, seedPointZ, (float)random.NextDouble() * 0.5F + 0.5F, yawAngle - ((float)Math.PI / 2F), pitchAngle / 3.0F, smallRange, bigRange, 1.0D);
                    AddTunnel(random.Next(), centerChunkX, centerChunkZ, chunk, biome, seedPointX, seedPointY, seedPointZ, (float)random.NextDouble() * 0.5F + 0.5F, yawAngle + ((float)Math.PI / 2F), pitchAngle / 3.0F, smallRange, bigRange, 1.0D);
                    return;
                }

                if (smallRangeIsNull || random.Next(4) != 0)
                {
                    double xDist = seedPointX - centerBlockX;
                    double zDist = seedPointZ - centerBlockZ;
                    double restRange = (double)(bigRange - smallRange);
                    double range = (double)(rangeScale + 2.0F + 16.0F);

                    if (xDist * xDist + zDist * zDist - restRange * restRange > range * range)
                    {
                        return;
                    }

                    // 种子点在中心方块附近（不在的话说明不在这个区块内不用管）
                    if (seedPointX >= centerBlockX - 16.0D - xzRange * 2.0D
                        && seedPointZ >= centerBlockZ - 16.0D - xzRange * 2.0D
                        && seedPointX <= centerBlockX + 16.0D + xzRange * 2.0D
                        && seedPointZ <= centerBlockZ + 16.0D + xzRange * 2.0D)
                    {
                        int startX = (int)Math.Floor(seedPointX - xzRange) - centerChunkX * 16 - 1;
                        int endX = (int)Math.Floor(seedPointX + xzRange) - centerChunkX * 16 + 1;
                        int endY = (int)Math.Floor(seedPointY - yRange) - 1;
                        int startY = (int)Math.Floor(seedPointY + yRange) + 1;
                        int startZ = (int)Math.Floor(seedPointZ - xzRange) - centerChunkZ * 16 - 1;
                        int endZ = (int)Math.Floor(seedPointZ + xzRange) - centerChunkZ * 16 + 1;

                        // 限制坐标范围
                        if (startX < 0)
                        {
                            startX = 0;
                        }

                        if (endX > 16)
                        {
                            endX = 16;
                        }

                        if (endY < 1)
                        {
                            endY = 1;
                        }

                        if (startY > 248)
                        {
                            startY = 248;
                        }

                        if (startZ < 0)
                        {
                            startZ = 0;
                        }

                        if (endZ > 16)
                        {
                            endZ = 16;
                        }

                        // 判断是不是海洋，如果在海洋则不生成洞穴
                        bool isOcean = false;

                        for (int x = startX; !isOcean && x < endX; ++x)
                        {
                            for (int z = startZ; !isOcean && z < endZ; ++z)
                            {
                                for (int y = startY + 1; !isOcean && y >= endY - 1; --y)
                                {
                                    if (y >= 0 && y < 256)
                                    {
                                        if (chunk[x, y, z].Id == (uint)BlockId.Water)
                                        {
                                            isOcean = true;
                                        }

                                        // 只判断边界
                                        if (y != endY - 1
                                            && x != startX
                                            && x != endX - 1
                                            && z != startZ
                                            && z != endZ - 1)
                                        {
                                            y = endY;
                                        }
                                    }
                                }
                            }
                        }

                        if (!isOcean)
                        {
                            // 挖掉一个椭球内的方块
                            for (int x = startX; x < endX; ++x)
                            {
                                // （归一化的距离）
                                double xDist1 = ((double)(x + centerChunkX * 16) + 0.5D - seedPointX) / xzRange;

                                for (int z = startZ; z < endZ; ++z)
                                {
                                    double zDist1 = ((double)(z + centerChunkZ * 16) + 0.5D - seedPointZ) / xzRange;
                                    bool isTopBlock = false;

                                    // 平面上平方距离<1
                                    if (xDist1 * xDist1 + zDist1 * zDist1 < 1.0D)
                                    {
                                        // 先获取高度(改为二分)
                                        int height = 64;
                                        int upBound = 255, downbound = 1;

                                        while (upBound != downbound)
                                        {
                                            int y = (upBound + downbound) / 2;
                                            if (chunk[x, y, z].IsAir() && !chunk[x, y - 1, z].IsAir())
                                            {
                                                height = y;
                                                break;
                                            }
                                            else if (chunk[x, y, z].IsAir())
                                            {
                                                upBound = y - 1;
                                            }
                                            else
                                            {
                                                downbound = y + 1;
                                            }
                                        }

                                        for (int y = startY; y > endY; --y)
                                        {
                                            double yDist = ((double)(y - 1) + 0.5D - seedPointY) / yRange;

                                            // 空间平方距离<1
                                            if (yDist > -0.7D)
                                            {
                                                if (xDist1 * xDist1 + yDist * yDist + zDist1 * zDist1 < 1.0D)
                                                {
                                                    BlockState curBlock = chunk[x, y, z];
                                                    BlockState upBlock = chunk[x, y + 1, z];

                                                    if (y == height - 1)
                                                    {
                                                        isTopBlock = true;
                                                    }

                                                    // 把这个方块替换为空气或岩浆
                                                    DigBlock(chunk, biome, x, y, z, centerChunkX, centerChunkZ, isTopBlock, curBlock, upBlock);
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (smallRangeIsNull)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        // 判断是否可以挖开这个方块
        protected bool CanReplaceBlock(BlockState curBlock, BlockState upBlock)
        {
            return _canReplaceSet.Contains(curBlock.Id) ||
                    ((curBlock.Id == (uint)BlockId.Sand
                        || curBlock.Id == (uint)BlockId.Gravel)
                    && upBlock.Id != (uint)BlockId.Water);
        }

        // 挖洞，默认参数
        protected void AddTunnel(int seed, int centerChunkX, int centerChunkZ, ChunkColumnStorage chunk, Biome biome, double seedPointX, double seedPointY, double seedPointZ)
        {
            AddTunnel(seed, centerChunkX, centerChunkZ, chunk, biome, seedPointX, seedPointY, seedPointZ, 1.0F + (float)_rand.NextDouble() * 6.0F, 0.0F, 0.0F, -1, -1, 0.5D);
        }

        protected void DigBlock(ChunkColumnStorage chunk, Biome biome, int x, int y, int z, int chunkX, int chunkZ, bool foundTop, BlockState state, BlockState up)
        {
            BlockState top = biome._topBlock;
            BlockState filler = biome._fillerBlock;
            if (CanReplaceBlock(state, up)
                || state == top
                || state == filler)
            {
                // y<10放置岩浆
                if (y < 10)
                {
                    // 设置为岩浆
                    chunk[x, y, z] = BlockStates.Lava();
                }
                else
                {
                    // 设置为空气
                    chunk[x, y, z] = BlockStates.Air();

                    if (up.Id == (uint)BlockId.Sand)
                    {
                        // 如果上面的方块是沙子则替换为沙石
                        chunk[x, y + 1, z] = BlockStates.Sandstone();
                    }

                    if (foundTop && chunk[x, y - 1, z] == filler)
                    {
                        // 如果挖开了顶层方块则把下面的方块设置为biome顶层方块
                        chunk[x, y - 1, z] = top;
                    }
                }
            }
        }
    }
}
