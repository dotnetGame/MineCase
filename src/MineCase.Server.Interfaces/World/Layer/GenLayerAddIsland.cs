using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.Biomes;

namespace MineCase.Server.World.Layer
{
    public class GenLayerAddIsland : GenLayer
    {
        public GenLayerAddIsland(int seed, GenLayer parent)
            : base(seed, parent)
        {
        }

        public override int[,] GetInts(int areaX, int areaY, int areaWidth, int areaHeight)
        {
            int parentAreaX = areaX - 1;
            int parentAreaY = areaY - 1;
            int parentWidth = areaWidth + 2;
            int parentHeight = areaHeight + 2;
            int[,] parentRes = _parent.GetInts(parentAreaX, parentAreaY, parentWidth, parentHeight);
            int[,] result = new int[areaHeight, areaWidth];

            for (int y = 0; y < areaHeight; ++y)
            {
                for (int x = 0; x < areaWidth; ++x)
                {
                    // 以x+1 y+1为中心，X型采样5个点
                    int parentValue = parentRes[y, x];
                    int parentValueX2 = parentRes[y, x + 2];
                    int parentValueY2 = parentRes[y + 2, x];
                    int parentValueX2Y2 = parentRes[y + 2, x + 2];
                    int parentValueX1Y1 = parentRes[y + 1, x + 1];
                    int randomSeed = GetChunkSeed(x + areaX, y + areaY);
                    Random random = new Random(randomSeed);

                    // 中心不为0 或 周围全是0
                    if (parentValueX1Y1 != 0 || (parentValue == 0 && parentValueX2 == 0 && parentValueY2 == 0 && parentValueX2Y2 == 0))
                    {
                        // 中心>0 且 周围出现了0
                        if (parentValueX1Y1 > 0 && (parentValue == 0 || parentValueX2 == 0 || parentValueY2 == 0 || parentValueX2Y2 == 0))
                        {
                            // 1/5概率变为海洋
                            if (random.Next(5) == 0)
                            {
                                // 中心是森林则不变为海洋
                                if (parentValueX1Y1 == 4)
                                {
                                    result[y, x] = 4;
                                }
                                else
                                {
                                    result[y, x] = 0;
                                }
                            }
                            else
                            {
                                result[y, x] = parentValueX1Y1;
                            }
                        }
                        else
                        {
                            result[y, x] = parentValueX1Y1;
                        }
                    }
                    else
                    {
                        // 概率中的分母
                        int deno = 1;
                        int value = 1;

                        // 选择一个不为0的值，越往后重新选的概率越小
                        if (parentValue != 0 && random.Next(deno++) == 0)
                        {
                            value = parentValue;
                        }

                        if (parentValueX2 != 0 && random.Next(deno++) == 0)
                        {
                            value = parentValueX2;
                        }

                        if (parentValueY2 != 0 && random.Next(deno++) == 0)
                        {
                            value = parentValueY2;
                        }

                        if (parentValueX2Y2 != 0 && random.Next(deno++) == 0)
                        {
                            value = parentValueX2Y2;
                        }

                        // 1/3的概率设置为刚才选的值
                        if (random.Next(3) == 0)
                        {
                            result[y, x] = value;
                        }
                        else if (value == 4)
                        {
                            // 森林
                            result[y, x] = 4;
                        }
                        else
                        {
                            result[y, x] = 0;
                        }
                    }
                }
            }

            return result;
        }
    }
}
