using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Biomes;
using Orleans;

namespace MineCase.Server.World.Plants
{
    public class DoubleGrassGenerator : PlantsGenerator
    {
        private PlantsType _grassType;
        private int _grassMaxNum;

        public DoubleGrassGenerator(PlantsType type, int maxNum = 16)
        {
            _grassType = type;
            _grassMaxNum = maxNum;
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int num = random.Next(_grassMaxNum);
            for (int i = 0; i < num; ++i)
            {
                BlockWorldPos blockpos = BlockWorldPos.Add(pos, random.Next(8), random.Next(4), random.Next(8));
                BlockChunkPos chunkpos = blockpos.ToBlockChunkPos();
                if (chunk[chunkpos.X, chunkpos.Y, chunkpos.Z] == BlockStates.Air() &&
                    blockpos.Y < 254 &&
                    CanGrassGrow(_grassType, chunk, chunkpos))
                {
                    chunk[chunkpos.X, chunkpos.Y, chunkpos.Z] = BlockStates.LargeFlowers(LargeFlowerType.DoubleTallgrass);
                    chunk[chunkpos.X, chunkpos.Y + 1, chunkpos.Z] = BlockStates.LargeFlowers(LargeFlowerType.TopHalfFlag);
                }
            }
        }

        public bool CanGrassGrow(PlantsType type, ChunkColumnStorage chunk, BlockChunkPos pos)
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
