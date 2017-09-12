using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.BlockEntities
{
    internal abstract class BlockEntityGrain : Grain, IBlockEntity
    {
        protected IWorld World { get; private set; }

        protected BlockWorldPos Position { get; private set; }

        public override Task OnActivateAsync()
        {
            var keys = this.GetWorldAndBlockEntityPosition();
            World = GrainFactory.GetGrain<IWorld>(keys.worldKey);
            Position = keys.position;
            return base.OnActivateAsync();
        }

        public virtual Task Destroy()
        {
            return Task.CompletedTask;
        }
    }
}
