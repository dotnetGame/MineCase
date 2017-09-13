using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class GrassGenerator : PlantsGenerator
    {
        public GrassGenerator()
        {
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            BlockChunkPos chunkPos = pos.ToBlockChunkPos();
            int x = chunkPos.X;
            int y = chunkPos.Y;
            int z = chunkPos.Z;

            // TODO use block accessor
            if (chunk[x, y, z].IsAir() &&
                chunk[x, y - 1, z] == BlockStates.GrassBlock())
            {
                chunk[x, y, z] = BlockStates.Grass(GrassType.TallGrass);
            }
        }
    }
}
