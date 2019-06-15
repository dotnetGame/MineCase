using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
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

        Task<ChunkColumnCompactStorage> GetStateUnsafe();

        Task<BlockState> GetBlockState(int x, int y, int z);

        Task<BlockState> GetBlockStateUnsafe(int x, int y, int z);

        Task ApplyChangeUnsafe(List<BlockStateChange> blockChanges);

        Task SetBlockState(int x, int y, int z, BlockState blockState);

        Task SetBlockStateUnsafe(int x, int y, int z, BlockState blockState);

        Task<BiomeId> GetBlockBiome(int x, int z);

        Task<IBlockEntity> GetBlockEntity(int x, int y, int z);

        Task<int> GetGroundHeight(int x, int z);

        Task OnBlockNeighborChanged(int x, int y, int z, BlockWorldPos neighborPosition, BlockState oldState, BlockState newState);

        Task EnsureChunkPopulated();
    }
}
