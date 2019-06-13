using System;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class FlowersGenerator : PlantsGenerator
    {
        public FlowersGenerator()
        {
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            BlockChunkPos chunkPos = pos.ToBlockChunkPos();
            int x = chunkPos.X;
            int y = chunkPos.Y;
            int z = chunkPos.Z;

            // TODO use block accessor
            if (chunk[x, y, z].IsAir() &&
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
