using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm.World.Biomes;
using MineCase.Block;
using MineCase.Engine;
using MineCase.Server.World;
using MineCase.World;
using MineCase.World.Biomes;
using Orleans;

namespace MineCase.Server.Components
{
    internal class ChunkAccessorComponent : Component
    {
        public ChunkAccessorComponent(string name = "chunkAccessor")
            : base(name)
        {
        }

        public Task SetBlockState(BlockWorldPos pos, BlockState state)
        {
            BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
            ChunkWorldPos chunkWorldPos = pos.ToChunkWorldPos();
            IWorld world = AttachedObject.GetWorld();
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(chunkWorldPos.X, chunkWorldPos.Z));
            return GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockState(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z,
                state);
        }

        public Task<BlockState> GetBlockState(BlockWorldPos pos)
        {
            BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
            ChunkWorldPos chunkWorldPos = pos.ToChunkWorldPos();
            IWorld world = AttachedObject.GetWorld();
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(chunkWorldPos.X, chunkWorldPos.Z));
            return GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockState(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z);
        }

        public Task<BiomeId> GetBlockBiome(BlockWorldPos pos)
        {
            BlockChunkPos blockChunkPos = pos.ToBlockChunkPos();
            ChunkWorldPos chunkWorldPos = pos.ToChunkWorldPos();
            IWorld world = AttachedObject.GetWorld();
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(chunkWorldPos.X, chunkWorldPos.Z));
            return GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockBiome(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z);
        }

        public Task<IChunkColumn> GetChunk(ChunkWorldPos pos)
        {
            IWorld world = AttachedObject.GetWorld();
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(pos.X, pos.Z));
            return Task.FromResult(GrainFactory.GetGrain<IChunkColumn>(chunkColumnKey));
        }
    }
}
