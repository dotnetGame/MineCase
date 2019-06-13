using System;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class PlantsGenerator
    {
        protected virtual void TrySetBlock(ChunkColumnStorage chunk, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState state)
        {
            if (pos.ToChunkWorldPos() == chunkWorldPos)
            {
                BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
                chunk[blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z] = state;
            }
        }

        // protected virtual BlockState TryGetBlock(ChunkColumnStorage chunk, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        // {
        //    if (pos.ToChunkWorldPos() == chunkWorldPos)
        //    {
        //        BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
        //        return chunk[blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z];
        //    }
        //    return BlockState;
        // }
        public virtual void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
        }
    }
}
