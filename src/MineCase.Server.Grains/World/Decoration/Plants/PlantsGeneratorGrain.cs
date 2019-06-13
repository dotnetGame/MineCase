using System;
using System.Threading.Tasks;
using MineCase.Algorithm.World.Biomes;
using MineCase.Server.World;
using MineCase.Server.World.Decoration;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World.Decoration.Plants
{
    public abstract class PlantsGeneratorGrain : Grain, IPlantsGenerator
    {
        protected virtual void SetBlock(IWorld world, ChunkColumnCompactStorage chunk, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState state)
        {
            if (pos.ToChunkWorldPos() == chunkWorldPos)
            {
                BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
                chunk[blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z] = state;
            }
        }

        protected virtual Task<BlockState> GetBlock(IWorld world, ChunkColumnCompactStorage chunk, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            if (pos.ToChunkWorldPos() == chunkWorldPos)
            {
                BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
                return Task.FromResult(chunk[blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z]);
            }
            else
            {
                var chunkColumnKey = world.MakeAddressByPartitionKey(pos.ToChunkWorldPos());
                return GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockState(pos.X, pos.Y, pos.Z);
            }
        }

        public abstract Task GenerateSingle(IWorld world, ChunkColumnCompactStorage chunk, ChunkWorldPos chunkWorldPos, BlockWorldPos pos);

        public virtual async Task Generate(IWorld world, ChunkColumnCompactStorage chunk, ChunkWorldPos pos, int countPerChunk, int range)
        {
            int seed = await world.GetSeed();

            for (int x = -range; x <= range; ++x)
            {
                for (int z = -range; z <= range; ++z)
                {
                    int chunkSeed = (pos.X + x) ^ (pos.Z + z) ^ seed;
                    Random rand = new Random(chunkSeed);
                    for (int count = 0; count < countPerChunk; ++count)
                    {
                        await GenerateSingle(world, chunk, pos, new BlockWorldPos { X = rand.Next(16), Z = rand.Next(16) });
                    }
                }
            }
        }
    }
}
