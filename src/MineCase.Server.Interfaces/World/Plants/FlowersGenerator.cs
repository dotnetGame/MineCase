using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class FlowersGenerator : PlantsGenerator
    {
        public int FlowersPerChunk { get; set; }

        public FlowersGenerator(int flowerPerChunk = 1)
        {
            FlowersPerChunk = flowerPerChunk;
        }

        public override Task Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
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
                        chunk[x, y - 1, z] == BlockStates.GrassBlock())
                    {
                        var flowerType = biome.GetRandomFlower(random);
                        switch (flowerType)
                        {
                            case PlantsType.RedFlower:
                                chunk[x, y, z] = BlockStates.Poppy();
                                break;
                            case PlantsType.YellowFlower:
                                chunk[x, y, z] = BlockStates.Dandelion();
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
