using System;
using MineCase.World.Biomes;

namespace MineCase.Algorithm.World.Layer
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
                    Random random = new Random(GetChunkSeed(areaX + j, areaY + i));
                    if (parentResult[i, j] == (int)BiomeId.Plains)
                    {
                        int r = random.Next(10);
                        if (r >= 0 && r < 1)
                        {
                            parentResult[i, j] = (int)BiomeId.Forest;
                        }
                        else if (r >= 1 && r < 2)
                        {
                            parentResult[i, j] = (int)BiomeId.ExtremeHills;
                        }
                        else if (r >= 2 && r < 3)
                        {
                            parentResult[i, j] = (int)BiomeId.Swampland;
                        }
                        else if (r >= 3 && r < 5)
                        {
                            parentResult[i, j] = (int)BiomeId.Desert;
                        }
                        else if (r >= 5 && r < 6)
                        {
                            parentResult[i, j] = (int)BiomeId.Taiga;
                        }
                    }
                }
            }

            return parentResult;
        }
    }
}
