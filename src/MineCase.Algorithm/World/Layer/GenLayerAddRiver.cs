using System;
using MineCase.World.Biomes;

namespace MineCase.Algorithm.World.Layer
{
    public class GenLayerAddRiver : GenLayer
    {
        public GenLayerAddRiver(int seed, GenLayer parent)
            : base(seed, parent)
        {
        }

        public override int[,] GetInts(int areaX, int areaY, int areaWidth, int areaHeight)
        {
            int parentAreaX = areaX - 1;
            int parentAreaY = areaY - 1;
            int parentWidth = areaWidth + 2;
            int parentHeight = areaHeight + 2;

            // parent是经过放大的GenLayerRiverInit
            int[,] parentRes = _parent.GetInts(parentAreaX, parentAreaY, parentWidth, parentHeight);
            int[,] result = new int[areaHeight, areaWidth];

            for (int y = 0; y < areaHeight; ++y)
            {
                for (int x = 0; x < areaWidth; ++x)
                {
                    // 由上层决定的0、2、3随机数
                    int randomValueY1 = parentRes[y + 1, x];
                    int randomValueX2Y1 = parentRes[y + 1, x + 2];
                    int randomValueX1 = parentRes[y, x + 1];
                    int randomValueX1Y2 = parentRes[y + 2, x + 1];
                    int randomValueX1Y1 = parentRes[y + 1, x + 1];

                    int tmpx, tmpy;
                    if (x + areaX >= 0) tmpx = (x + areaX) / 64;
                    else tmpx = (x + areaX) / 64 - 1;
                    if (y + areaY >= 0) tmpy = (y + areaY) / 64;
                    else tmpy = (y + areaY) / 64 - 1;

                    int seed = GetChunkSeed(tmpx, tmpy);
                    Random rand = new Random(seed);

                    if (randomValueX1Y1 == randomValueY1
                        && randomValueX1Y1 == randomValueX1
                        && randomValueX1Y1 == randomValueX2Y1
                        && randomValueX1Y1 == randomValueX1Y2)
                    {
                        // 中心和周围相等
                        result[y, x] = randomValueX1Y1;
                    }
                    else if (rand.Next(5) == 0)
                    {
                        // 河流
                        if (randomValueX1Y1 != (int)BiomeId.Ocean &&
                            randomValueX1Y1 != (int)BiomeId.Beach)
                            result[y, x] = (int)BiomeId.River;
                    }
                    else
                    {
                        result[y, x] = randomValueX1Y1;
                    }
                }
            }

            return result;
        }

        // value >= 2则返回2~3的随机数，否则返回value
        private int RandomValue(int value)
        {
            return value >= 2 ? 2 + value % 2 : value;
        }
    }
}
