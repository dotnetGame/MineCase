using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class WorldAccessorGrain : Grain, IWorldAccessor
    {
        private const string _defaultWorldName = "defaultWorld";

        public Task<IWorld> GetDefaultWorld()
        {
            return Task.FromResult(GrainFactory.GetGrain<IWorld>(_defaultWorldName));
        }

        public Task<IWorld> GetWorld(string name)
        {
            return Task.FromResult(GrainFactory.GetGrain<IWorld>(name));
        }
    }
}
