using System;
using MineCase.Algorithm.World.Biomes;
using MineCase.Block;
using MineCase.Server.World;
using MineCase.World;
using MineCase.World.Plants;
using Orleans;

namespace MineCase.Algorithm.World.Plants
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

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int num = random.Next(_flowersMaxNum);
            for (int i = 0; i < num; ++i)
            {
                BlockWorldPos blockpos = BlockWorldPos.Add(pos, random.Next(8), random.Next(4), random.Next(8));
                BlockChunkPos chunkpos = blockpos.ToBlockChunkPos();
                if (chunk[chunkpos.X, chunkpos.Y, chunkpos.Z].IsAir() &&
                    blockpos.Y < 254 &&
                    CanFlowerGrow(_flowerType, chunk, chunkpos))
                {
                    chunk[chunkpos.X, chunkpos.Y, chunkpos.Z] = BlockStates.Sunflower(SunflowerHalfType.Lower);
                    chunk[chunkpos.X, chunkpos.Y + 1, chunkpos.Z] = BlockStates.Sunflower(SunflowerHalfType.Upper);
                }
            }
        }

        public bool CanFlowerGrow(PlantsType type, ChunkColumnCompactStorage chunk, BlockChunkPos pos)
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
