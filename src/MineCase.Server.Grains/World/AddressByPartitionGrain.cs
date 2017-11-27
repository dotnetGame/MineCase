using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World
{
    internal abstract class AddressByPartitionGrain : Persistence.PersistableDependencyObject, IAddressByPartition
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
