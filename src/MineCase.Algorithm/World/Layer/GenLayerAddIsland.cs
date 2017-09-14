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
                    // ��x+1 y+1Ϊ���ģ�X�Ͳ���5����
                    int parentValue = parentRes[y, x];
                    int parentValueX2 = parentRes[y, x + 2];
                    int parentValueY2 = parentRes[y + 2, x];
                    int parentValueX2Y2 = parentRes[y + 2, x + 2];
                    int parentValueX1Y1 = parentRes[y + 1, x + 1];
                    int randomSeed = GetChunkSeed(x + areaX, y + areaY);
                    Random random = new Random(randomSeed);

                    // ���Ĳ�Ϊ0 �� ��Χȫ��0
                    if (parentValueX1Y1 != 0 || (parentValue == 0 && parentValueX2 == 0 && parentValueY2 == 0 && parentValueX2Y2 == 0))
                    {
                        // ����>0 �� ��Χ������0
                        if (parentValueX1Y1 > 0 && (parentValue == 0 || parentValueX2 == 0 || parentValueY2 == 0 || parentValueX2Y2 == 0))
                        {
                            // 1/5���ʱ�Ϊ����
                            if (random.Next(5) == 0)
                            {
                                // ������ɭ���򲻱�Ϊ����
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
                        // �����еķ�ĸ
                        int deno = 1;
                        int value = 1;

                        // ѡ��һ����Ϊ0��ֵ��Խ��������ѡ�ĸ���ԽС
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

                        // 1/3�ĸ�������Ϊ�ղ�ѡ��ֵ
                        if (random.Next(3) == 0)
                        {
                            result[y, x] = value;
                        }
                        else if (value == 4)
                        {
                            // ɭ��
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
