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
        protected virtual Task SetBlock(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos, BlockState state)
        {
            var chunkColumnKey = world.MakeAddressByPartitionKey(pos.ToChunkWorldPos());
            var chunkGrain = GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey);
            if (pos.ToChunkWorldPos() == chunkWorldPos)
            {
                BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
                return chunkGrain.SetBlockStateUnsafe(blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z, state);
            }

            return Task.CompletedTask;
        }

        protected virtual Task<BlockState> GetBlock(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos)
        {
            var chunkColumnKey = world.MakeAddressByPartitionKey(pos.ToChunkWorldPos());
            var chunkGrain = GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey);
            BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();

            return chunkGrain.GetBlockStateUnsafe(blockChunkPos.X, blockChunkPos.Y, blockChunkPos.Z);
        }

        private Task<int> GetGroundHeight(IWorld world, ChunkWorldPos chunkWorldPos, int x, int z)
        {
            var blockChunkPos = new BlockWorldPos { X = x, Z = z }.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(chunkWorldPos);
            return GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetGroundHeight(blockChunkPos.X, blockChunkPos.Z);
        }

        public abstract Task GenerateSingle(IWorld world, ChunkWorldPos chunkWorldPos, BlockWorldPos pos);

        public virtual async Task Generate(IWorld world, ChunkWorldPos pos, int countPerChunk, int range)
        {
            int seed = await world.GetSeed();

            for (int chunkXOffset = -range; chunkXOffset <= range; ++chunkXOffset)
            {
                for (int chunkZOffset = -range; chunkZOffset <= range; ++chunkZOffset)
                {
                    ChunkWorldPos curChunkWorldPos = new ChunkWorldPos { X = pos.X + chunkXOffset, Z = pos.Z + chunkZOffset };
                    BlockWorldPos curChunkCorner = curChunkWorldPos.ToBlockWorldPos();

                    int chunkSeed = (pos.X + chunkXOffset) ^ (pos.Z + chunkZOffset) ^ seed;
                    Random rand = new Random(chunkSeed);

                    for (int count = 0; count < countPerChunk; ++count)
                    {
                        int x = curChunkCorner.X + rand.Next(16);
                        int z = curChunkCorner.Z + rand.Next(16);
                        int groundHeight = await GetGroundHeight(world, pos, x, z);
                        await GenerateSingle(world, pos, new BlockWorldPos { X = x, Y = groundHeight, Z = z });
                    }
                }
            }
        }
    }
}
