﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class GrassGenerator : PlantsGenerator
    {
        public int GrassPerChunk { get; set; }

        public GrassGenerator(int grassPerChunk = 1)
        {
            GrassPerChunk = grassPerChunk;
        }

        public override async Task Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            var blockAccessor = await world.GetBlockAccessor();
            int grassMaxNum = random.Next(GrassPerChunk);
            for (int grassNum = 0; grassNum < grassMaxNum; ++grassNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
                for (int y = 255; y >= 1; --y)
                {
                    // TODO use block accessor
                    if (chunk[x, y, z] == BlockStates.Air() &&
                        chunk[x, y - 1, z] == BlockStates.GrassBlock())
                    {
                        chunk[x, y, z] = BlockStates.Grass(GrassType.TallGrass);
                        break;
                    }
                }
            }
        }
    }
}
