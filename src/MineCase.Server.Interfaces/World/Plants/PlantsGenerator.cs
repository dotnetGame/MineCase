using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;

namespace MineCase.Server.World.Plants
{
    public class PlantsGenerator
    {
        public virtual Task Generate(IWorld world, ChunkColumnStorage chunk, Biome biome, Random random, BlockPos pos)
        {
            return Task.CompletedTask;
        }
    }
}
