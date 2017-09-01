using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class PlantsGenerator
    {
        public virtual Task Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockPos pos)
        {
            return Task.CompletedTask;
        }
    }
}
