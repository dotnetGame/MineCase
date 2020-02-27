using System;
using MineCase.World.Biomes;

namespace MineCase.Algorithm.World.Layer
{
    public class GenLayerAddBeach : GenLayer
    {
        public GenLayerAddBeach(int seed, GenLayer parent)
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

                    // Random random = new Random(randomSeed);

                    // 中心不为0 且 周围有0
                    if (parentValueX1Y1 != 0 && (parentValue == 0 || parentValueX2 == 0 || parentValueY2 == 0 || parentValueX2Y2 == 0))
                    {
                        if (parentValueX1Y1 != (int)BiomeId.Mountains)
                            result[y, x] = (int)BiomeId.Beach;
                    }
                    else
                    {
                        result[y, x] = parentValueX1Y1;
                    }
                }
            }

            return result;
        }
    }
}
