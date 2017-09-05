using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Layer
{
    public class GenLayerIsland : GenLayer
    {
        public GenLayerIsland(int seed, GenLayer parent)
            : base(seed, parent)
        {
        }

        public override int[,] GetInts(int areaX, int areaY, int areaWidth, int areaHeight)
        {
            int[,] result = new int[areaHeight, areaWidth];

            for (int i = 0; i < areaHeight; ++i)
            {
                for (int j = 0; j < areaWidth; ++j)
                {
                    Random random = new Random((areaY + i) * 16384 + areaX + j);
                    result[i, j] = random.Next(2) == 1 ? 1 : 0;
                }
            }

            return result;
        }
    }
}
