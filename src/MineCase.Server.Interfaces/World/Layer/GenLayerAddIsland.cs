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
            int[,] parentResult = _parent.GetInts(areaX, areaY, areaWidth + 2, areaHeight + 2);
            for (int i = 1; i < areaHeight - 1; ++i)
            {
                for (int j = 1; j < areaWidth - 1; ++j)
                {
                    Random random = new Random((areaY + i) * 16384 + areaX + j);
                    if (parentResult[i + 1, j] == (int)BiomeId.Plains ||
                        parentResult[i - 1, j] == (int)BiomeId.Plains ||
                        parentResult[i, j + 1] == (int)BiomeId.Plains ||
                        parentResult[i, j - 1] == (int)BiomeId.Plains)
                    {
                        if (random.Next(3) == 0)
                            parentResult[i - 1, j - 1] = (int)BiomeId.Plains;
                    }
                }
            }

            return parentResult;
        }
    }
}
