using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Server.Game.BlockEntities;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World
{
    public static class WorldExtensions
    {
        /// <summary>
        /// Worlds to chunk.世界坐标转区块坐标.
        /// </summary>
        /// <param name="n">The n.</param>
        public static int WorldToChunk(int n)
        {
            int chunkPos = n / ChunkConstants.BlockEdgeWidthInSection;
            if (n % ChunkConstants.BlockEdgeWidthInSection < 0) chunkPos -= 1;
            return chunkPos;
        }

        /// <summary>
        /// Worlds to block.世界坐标转区块内坐标.
        /// </summary>
        /// <param name="n">The n.</param>
        public static int WorldToBlock(int n)
        {
            int blockPos = n % ChunkConstants.BlockEdgeWidthInSection;
            if (blockPos < 0) blockPos += ChunkConstants.BlockEdgeWidthInSection;
            return blockPos;
        }

        /// <summary>
        /// Gets the state of the block.
        /// </summary>
        /// <param name="world">The world Grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns>方块类型.</returns>
        public static Task<BlockState> GetBlockState(this IWorld world, IGrainFactory grainFactory, int x, int y, int z)
        {
            var xOffset = MakeRelativeBlockOffset(x);
            var zOffset = MakeRelativeBlockOffset(z);
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(xOffset.Chunk, zOffset.Chunk));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockState(
                xOffset.Block,
                y,
                zOffset.Block);
        }

        /// <summary>
        /// Gets the state of the block.
        /// </summary>
        /// <param name="world">The world Grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns>方块类型.</returns>
        public static Task<BlockState> GetBlockState(this IWorld world, IGrainFactory grainFactory, BlockWorldPos pos)
        {
            var chunkPos = pos.ToChunkWorldPos();
            var blockChunkPos = pos.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(chunkPos);
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockState(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z);
        }

        /// <summary>
        /// Gets the state of the block. Only used in ***decoration*** stage.
        /// </summary>
        /// <param name="world">The world Grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns>方块类型.</returns>
        public static Task<BlockState> GetBlockStateUnsafe(this IWorld world, IGrainFactory grainFactory, int x, int y, int z)
        {
            var blockWorldPos = new BlockWorldPos(x, y, z);
            var blockChunkPos = blockWorldPos.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(blockWorldPos.ToChunkWorldPos());
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockStateUnsafe(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z);
        }

        /// <summary>
        /// Gets the state of the block. Only used in ***decoration*** stage.
        /// </summary>
        /// <param name="world">The world Grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns>方块类型.</returns>
        public static Task<BlockState> GetBlockStateUnsafe(this IWorld world, IGrainFactory grainFactory, BlockWorldPos pos)
        {
            var blockChunkPos = pos.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(pos.ToChunkWorldPos());
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockStateUnsafe(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z);
        }

        /// <summary>
        /// Sets the state of the block.
        /// </summary>
        /// <param name="world">The world grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="state">The state.</param>
        public static Task SetBlockState(this IWorld world, IGrainFactory grainFactory, int x, int y, int z, BlockState state)
        {
            var xOffset = MakeRelativeBlockOffset(x);
            var zOffset = MakeRelativeBlockOffset(z);
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(xOffset.Chunk, zOffset.Chunk));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockState(
                xOffset.Block,
                y,
                zOffset.Block,
                state);
        }

        /// <summary>
        /// Sets the state of the block.
        /// </summary>
        /// <param name="world">The world grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="pos">The position.</param>
        /// <param name="state">The state.</param>
        public static Task SetBlockState(this IWorld world, IGrainFactory grainFactory, BlockWorldPos pos, BlockState state)
        {
            var chunkPos = pos.ToChunkWorldPos();
            var blockChunkPos = pos.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(chunkPos);
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockState(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z,
                state);
        }

        /// <summary>
        /// Sets the state of the block. Only used in ***decoration*** stage.
        /// </summary>
        /// <param name="world">The world grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="state">The state.</param>
        public static Task SetBlockStateUnsafe(this IWorld world, IGrainFactory grainFactory, int x, int y, int z, BlockState state)
        {
            var blockWorldPos = new BlockWorldPos(x, y, z);
            var blockChunkPos = blockWorldPos.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(blockWorldPos.ToChunkWorldPos());
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockStateUnsafe(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z,
                state);
        }

        /// <summary>
        /// Sets the state of the block. Only used in ***decoration*** stage.
        /// </summary>
        /// <param name="world">The world grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="pos">The position.</param>
        /// <param name="state">The state.</param>
        public static Task SetBlockStateUnsafe(this IWorld world, IGrainFactory grainFactory, BlockWorldPos pos, BlockState state)
        {
            var blockChunkPos = pos.ToBlockChunkPos();
            var chunkColumnKey = world.MakeAddressByPartitionKey(pos.ToChunkWorldPos());
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockStateUnsafe(
                blockChunkPos.X,
                blockChunkPos.Y,
                blockChunkPos.Z,
                state);
        }

        public static async Task ApplyChangeUnsafe(this IWorld world, IGrainFactory grainFactory, BatchBlockChange change)
        {
            var partitionChange = change.GetByPartition();
            foreach (var eachPartitionChange in partitionChange)
            {
                var chunkColumnKey = world.MakeAddressByPartitionKey(eachPartitionChange.Key);
                await grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).ApplyChangeUnsafe(eachPartitionChange.Value);
            }
        }

        public static async Task<int> GetHeight(this IWorld world, IGrainFactory grainFactory, BlockWorldPos pos)
        {
            var xOffset = MakeRelativeBlockOffset(pos.X);
            var zOffset = MakeRelativeBlockOffset(pos.Z);
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(xOffset.Chunk, zOffset.Chunk));
            var chunk = grainFactory.GetGrain<IChunkColumn>(chunkColumnKey);
            int y;
            for (y = 255; y >= 0; --y)
            {
                if (!(await chunk.GetBlockState(xOffset.Block, y, zOffset.Block)).IsAir())
                {
                    break;
                }
            }

            return y + 1;
        }

        public static (int Chunk, int Block) MakeRelativeBlockOffset(int value)
        {
            var chunk = value / ChunkConstants.BlockEdgeWidthInSection;
            var block = value % ChunkConstants.BlockEdgeWidthInSection;
            if (block < 0)
            {
                chunk--;
                block += ChunkConstants.BlockEdgeWidthInSection;
            }

            return (chunk, block);
        }

        /// <summary>
        /// Gets the state of the block.
        /// </summary>
        /// <param name="world">The world Grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <returns>方块类型.</returns>
        public static Task<IBlockEntity> GetBlockEntity(this IWorld world, IGrainFactory grainFactory, int x, int y, int z)
        {
            var xOffset = MakeRelativeBlockOffset(x);
            var zOffset = MakeRelativeBlockOffset(z);
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(xOffset.Chunk, zOffset.Chunk));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockEntity(
                xOffset.Block,
                y,
                zOffset.Block);
        }

        /// <summary>
        /// Gets the state of the block.
        /// </summary>
        /// <param name="world">The world Grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="pos">The position.</param>
        /// <returns>方块类型.</returns>
        public static Task<IBlockEntity> GetBlockEntity(this IWorld world, IGrainFactory grainFactory, BlockWorldPos pos)
        {
            var xOffset = MakeRelativeBlockOffset(pos.X);
            var zOffset = MakeRelativeBlockOffset(pos.Z);
            var chunkColumnKey = world.MakeAddressByPartitionKey(new ChunkWorldPos(xOffset.Chunk, zOffset.Chunk));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockEntity(
                xOffset.Block,
                pos.Y,
                zOffset.Block);
        }
    }
}
