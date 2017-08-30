using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.World.Biomes;

namespace MineCase.Server.World.Plants
{
    public class PlantsGenerator
    {
        public virtual void Generate(IWorld world, Biome biome, Random random, BlockPos pos)
        {
        }
    }
}
