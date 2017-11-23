using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.World;
using MineCase.World.Biomes;
using Orleans;

namespace MineCase.Server.World
{
    public interface IChunkColumn : IAddressByPartition
    {
        Task<ChunkColumnCompactStorage> GetState();

        Task<BlockState> GetBlockState(int x, int y, int z);

        Task SetBlockState(int x, int y, int z, BlockState blockState);

        Task<BiomeId> GetBlockBiome(int x, int z);

        Task<IBlockEntity> GetBlockEntity(int x, int y, int z);

        Task OnBlockNeighborChanged(int x, int y, int z, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState);

        Task OnGameTick(TimeSpan deltaTime, long worldAge);
    }
}
