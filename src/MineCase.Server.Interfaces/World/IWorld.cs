using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using Orleans;

namespace MineCase.Server.World
{
    public interface IWorld : IGrainWithStringKey
    {
        Task<uint> NewEntityId();

        Task AttachEntity(IEntity entity);

        Task<IEntity> FindEntity(uint eid);

        Task<(long age, long timeOfDay)> GetTime();

        Task<long> GetAge();

        Task OnGameTick(TimeSpan deltaTime);
    }

    public static class WorldExtensions
    {
        public static Task<BlockState> GetBlockState(this IWorld world, IGrainFactory grainFactory, int x, int y, int z)
        {
            var xOffset = MakeRelativeBlockOffset(x);
            var zOffset = MakeRelativeBlockOffset(z);
            var chunkColumnKey = world.MakeChunkColumnKey(xOffset.chunk, zOffset.chunk);
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).GetBlockState(xOffset.block, y, zOffset.block);
        }

        public static Task SetBlockState(this IWorld world, IGrainFactory grainFactory, int x, int y, int z, BlockState blockState)
        {
            var xOffset = MakeRelativeBlockOffset(x);
            var zOffset = MakeRelativeBlockOffset(z);
            var chunkColumnKey = world.MakeChunkColumnKey(xOffset.chunk, zOffset.chunk);
            return grainFactory.GetGrain<IChunkColumn>(chunkColumnKey).SetBlockState(xOffset.block, y, zOffset.block, blockState);
        }

        private static (int chunk, int block) MakeRelativeBlockOffset(int value)
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
    }
}
