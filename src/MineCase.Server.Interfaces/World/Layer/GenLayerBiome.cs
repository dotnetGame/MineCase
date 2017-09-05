using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.Biomes;

namespace MineCase.Server.World.Layer
{
    public class GenLayerBiome : GenLayer
    {
        public GenLayerBiome(int seed, GenLayer parent)
            : base(seed, parent)
        {
        }

        public override int[,] GetInts(int areaX, int areaY, int areaWidth, int areaHeight)
        {
            int[,] parentResult = _parent.GetInts(areaX, areaY, areaWidth, areaHeight);
            for (int i = 0; i < areaHeight; ++i)
            {
                for (int j = 0; j < areaWidth; ++j)
                {
                    Random random = new Random((areaY + i) * 16384 + areaX + j);
                    if (random.Next(20) == 0)
                        parentResult[i, j] = (int)BiomeId.Desert;
                }
            }

            return parentResult;
        }
    }
}
