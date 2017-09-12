using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World
{
    public interface IChunkColumn : IGrainWithStringKey
    {
        Task<ChunkColumnStorage> GetState();

        Task<BlockState> GetBlockState(int x, int y, int z);

        Task SetBlockState(int x, int y, int z, BlockState blockState);

        Task<IBlockEntity> GetBlockEntity(int x, int y, int z);

        Task OnBlockNeighborChanged(int x, int y, int z, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState);
    }

    public static class ChunkColumnExtensions
    {
        public static string MakeChunkColumnKey(this IWorld world, int x, int z)
        {
            return $"{world.GetPrimaryKeyString()},{x},{z}";
        }

        public static (int x, int z) GetChunkPosition(this IChunkColumn chunkColumn)
        {
            var key = chunkColumn.GetPrimaryKeyString().Split(',');
            return (int.Parse(key[1]), int.Parse(key[2]));
        }

        public static (string worldKey, int x, int z) GetWorldAndChunkPosition(this IChunkColumn chunkColumn)
        {
            var key = chunkColumn.GetPrimaryKeyString().Split(',');
            return (key[0], int.Parse(key[1]), int.Parse(key[2]));
        }
    }
}
