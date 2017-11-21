using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Persistence
{
    internal abstract class PersistableAddressByPartitionGrain<TState> : PersistableGrain<TState>, IAddressByPartition
        where TState : PersistableStateBase
    {
        protected IWorld World { get; private set; }

        protected ChunkWorldPos ChunkWorldPos { get; private set; }

        public override Task OnActivateAsync()
        {
            var keys = this.GetWorldAndChunkWorldPos();
            World = GrainFactory.GetGrain<IWorld>(keys.worldKey);
            ChunkWorldPos = keys.chunkWorldPos;
            return base.OnActivateAsync();
        }
    }
}
