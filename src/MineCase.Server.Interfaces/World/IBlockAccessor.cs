using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World
{
    public interface IBlockAccessor : IGrainWithStringKey
    {
        Task<BlockState> GetBlockState(int x, int y, int z);

        Task SetBlockState(int x, int y, int z, BlockState state);

        Task<bool> CanBlockStay(int x, int y, int z, BlockState state);
    }
}
