using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World
{
    public static class WorldExtensions
    {
        /// <summary>
        /// Worlds to chunk.世界坐标转区块坐标
        /// </summary>
        /// <param name="n">The n.</param>
        public static int WorldToChunk(int n)
        {
            int chunkPos = n / ChunkConstants.BlockEdgeWidthInSection;
            if (chunkPos < 0) chunkPos -= 1;
            return chunkPos;
        }

        /// <summary>
        /// Worlds to block.世界坐标转区块内坐标
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
        /// <returns>方块类型</returns>
        public static Task<BlockState> GetBlockState(this IWorld world, IGrainFactory grainFactory, int x, int y, int z)
        {
            var chunkColumnKey = world.MakeChunkColumnKey(WorldToChunk(x), WorldToChunk(z));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockState(
                WorldToBlock(x),
                y,
                WorldToBlock(z));
        }

        /// <summary>
        /// Gets the state of the block.
        /// </summary>
        /// <param name="world">The world Grain.</param>
        /// <param name="grainFactory">The grain factory.</param>
        /// <param name="pos">The position.</param>
        /// <returns>方块类型</returns>
        public static Task<BlockState> GetBlockState(this IWorld world, IGrainFactory grainFactory, BlockWorldPos pos)
        {
            var chunkColumnKey = world.MakeChunkColumnKey(WorldToChunk(pos.X), WorldToChunk(pos.Z));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockState(
                WorldToBlock(pos.X),
                pos.Y,
                WorldToBlock(pos.Z));
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
            var chunkColumnKey = world.MakeChunkColumnKey(WorldToChunk(x), WorldToChunk(z));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockState(
                WorldToBlock(x),
                y,
                WorldToBlock(z),
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
            var chunkColumnKey = world.MakeChunkColumnKey(WorldToChunk(pos.X), WorldToChunk(pos.Z));
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockState(
                WorldToBlock(pos.X),
                pos.Y,
                WorldToBlock(pos.Z),
                state);
        }
    }
}
