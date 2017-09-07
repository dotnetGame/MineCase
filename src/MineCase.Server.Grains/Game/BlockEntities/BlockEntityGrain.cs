using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Game.BlockEntities
{
    internal abstract class BlockEntityGrain : Grain, IBlockEntity
    {
        public virtual Task Destroy()
        {
            return Task.CompletedTask;
        }
    }
}
