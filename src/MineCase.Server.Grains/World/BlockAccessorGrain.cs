using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [StatelessWorker]
    public class BlockAccessorGrain : Grain, IBlockAccessor
    {
        private IWorld _world;

        public override Task OnActivateAsync()
        {
            _world = GrainFactory.GetGrain<IWorld>(this.GetPrimaryKeyString());
            return base.OnActivateAsync();
        }

        public async Task<BlockState> GetBlockState(int x, int y, int z)
        {
            // 需要优化？？
            var chunkColumn = GrainFactory.GetGrain<IChunkColumn>(_world.MakeChunkColumnKey(x, z));
            return await chunkColumn.GetBlockState(x, y, z);
        }

        public async Task SetBlockState(int x, int y, int z, BlockState state)
        {
            // 需要优化？？
            var chunkColumn = GrainFactory.GetGrain<IChunkColumn>(_world.MakeChunkColumnKey(x, z));
            await chunkColumn.SetBlockState(state, x, y, z);
        }
    }
}
