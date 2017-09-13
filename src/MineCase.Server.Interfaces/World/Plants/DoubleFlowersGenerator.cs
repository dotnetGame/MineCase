using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class DoubleFlowersGenerator : PlantsGenerator
    {
        private PlantsType _flowerType;
        private int _flowersMaxNum;

        public DoubleFlowersGenerator(PlantsType type, int maxNum = 16)
        {
            _flowerType = type;
            _flowersMaxNum = maxNum;
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int num = random.Next(_flowersMaxNum);
            for (int i = 0; i < num; ++i)
            {
                BlockWorldPos blockpos = BlockWorldPos.Add(pos, random.Next(8), random.Next(4), random.Next(8));
                BlockChunkPos chunkpos = blockpos.ToBlockChunkPos();
                if (chunk[chunkpos.X, chunkpos.Y, chunkpos.Z] == BlockStates.Air() &&
                    blockpos.Y < 254 &&
                    CanFlowerGrow(_flowerType, chunk, chunkpos))
                {
                    chunk[chunkpos.X, chunkpos.Y, chunkpos.Z] = BlockStates.LargeFlowers(LargeFlowerType.Sunflower);
                    chunk[chunkpos.X, chunkpos.Y + 1, chunkpos.Z] = BlockStates.LargeFlowers(LargeFlowerType.TopHalfFlag);
                }
            }
        }

        public bool CanFlowerGrow(PlantsType type, ChunkColumnStorage chunk, BlockChunkPos pos)
        {
            if (chunk[pos.X, pos.Y - 1, pos.Z] == BlockStates.GrassBlock() ||
                chunk[pos.X, pos.Y - 1, pos.Z] == BlockStates.Dirt())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
