using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class FlowersGenerator : PlantsGenerator
    {
        public FlowersGenerator()
        {
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            BlockChunkPos chunkPos = pos.ToBlockChunkPos();
            int x = chunkPos.X;
            int y = chunkPos.Y;
            int z = chunkPos.Z;

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
            }
        }
    }
}
