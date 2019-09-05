using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Interfaces.World;
using Orleans;

namespace MineCase.Server.Grains.World
{
    public class WorldPartitionGrain : Grain, IWorldPartition
    {
        private EntityPack _entityPack = new EntityPack();

        public Task EnterEntity(Entity entity)
        {
            return Task.CompletedTask;
        }

        public Task LeaveEntity(Entity entity)
        {
            return Task.CompletedTask;
        }

        public Task OnTick()
        {
            return Task.CompletedTask;
        }
    }
}
