using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Player
{
    class NonAuthenticatedPlayerGrain : Grain, INonAuthenticatedPlayer
    {
        private Guid _uuid;

        public override Task OnActivateAsync()
        {
            _uuid = Guid.NewGuid();
            return base.OnActivateAsync();
        }

        public Task<Guid> GetUUID()
        {
            return Task.FromResult(_uuid);
        }
    }
}
