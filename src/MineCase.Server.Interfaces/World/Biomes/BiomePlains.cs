using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Biomes
{
    public class BiomePlains : Biome
    {
        public BiomePlains(BiomeProperties properties)
            : base(properties)
        {
        }

        // 使用植物进行装点修饰
        public override void Decorate(IWorld world, Random rand, BlockPos pos)
        {
            // 加草

            // 加花

            // 加树
        }

        // 使用花装点
        private void GenFlowers()
        {

        }
    }
}