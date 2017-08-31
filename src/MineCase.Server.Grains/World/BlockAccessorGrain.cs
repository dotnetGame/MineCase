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
            var chunkColumn = GrainFactory.GetGrain<IChunkColumn>(_world.MakeChunkColumnKey(x / 16, z / 16));
            int offsetX = x % 16;
            int offsetZ = z % 16;
            if (offsetX < 0) offsetX += 16;
            if (offsetZ < 0) offsetZ += 16;
            return await chunkColumn.GetBlockState(offsetX, y, offsetZ);
        }

        public async Task SetBlockState(int x, int y, int z, BlockState state)
        {
            // 需要优化？？
            var chunkColumn = GrainFactory.GetGrain<IChunkColumn>(_world.MakeChunkColumnKey(x / 16, z / 16));
            int offsetX = x % 16;
            int offsetZ = z % 16;
            if (offsetX < 0) offsetX += 16;
            if (offsetZ < 0) offsetZ += 16;
            await chunkColumn.SetBlockState(state, offsetX, y, offsetZ);
        }

        public async Task<bool> CanBlockStay(int x, int y, int z, BlockState state)
        {
            if (state == BlockStates.Tallgrass())
            {
                if (y > 0)
                {
                    var downState = await GetBlockState(x, y - 1, z);
                    if (downState == BlockStates.Dirt() ||
                        downState == BlockStates.Grass())
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
