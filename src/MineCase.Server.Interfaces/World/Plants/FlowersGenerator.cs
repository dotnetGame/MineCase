using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;

namespace MineCase.Server.World.Plants
{
    public class FlowersGenerator : PlantsGenerator
    {
        public int FlowersPerChunk { get; set; }

        public FlowersGenerator(int flowerPerChunk = 1)
        {
            FlowersPerChunk = flowerPerChunk;
        }

        public override Task Generate(IWorld world, ChunkColumnStorage chunk, Biome biome, Random random, BlockPos pos)
        {
            int flowerMaxNum = random.Next(FlowersPerChunk);
            for (int flowerNum = 0; flowerNum < flowerMaxNum; ++flowerNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
                for (int y = 255; y >= 1; --y)
                {
                    // TODO use block accessor
                    if (chunk[x, y, z] == BlockStates.Air() &&
                        chunk[x, y - 1, z] == BlockStates.Grass())
                    {
                        var flowerType = biome.GetRandomFlower(random);
                        switch (flowerType)
                        {
                            case PlantsType.RedFlower:
                                chunk[x, y, z] = BlockStates.RedFlower();
                                break;
                            case PlantsType.YellowFlower:
                                chunk[x, y, z] = BlockStates.YellowFlower();
                                break;
                        }

                        break;
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
