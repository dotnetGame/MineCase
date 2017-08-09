using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.World
{
    class WorldAccessorGrain : Grain, IWorldAccessor
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
