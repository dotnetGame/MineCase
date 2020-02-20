using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World
{

    public class WorldState
    {
        public long WorldAge { get; set; }

        public uint NextAvailEId { get; set; }
    }

    public class World : Grain<WorldState>, IWorld
    {
        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
        }

        public override async Task OnDeactivateAsync()
        {
            await WriteStateAsync();
            await base.OnDeactivateAsync();
        }

        public Task<long> GetAge()
        {
            return Task.FromResult(State.WorldAge);
        }
    }
}
