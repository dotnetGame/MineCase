using MineCase.Server.World;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entities
{
    internal abstract class EntityGrain : Grain, IEntity
    {
        protected IWorld World { get; private set; }
        protected uint EntityId { get; private set; }

        public override Task OnActivateAsync()
        {
            var keys = this.GetWorldAndEntityId();
            World = GrainFactory.GetGrain<IWorld>(keys.worldKey);
            EntityId = keys.entityId;
            return Task.CompletedTask;
        }
    }
}
