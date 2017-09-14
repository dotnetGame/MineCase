using System;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class PlantsGenerator
    {
        public virtual void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
        }
    }
}
