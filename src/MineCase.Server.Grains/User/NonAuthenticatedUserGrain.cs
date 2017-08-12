using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.User
{
    class NonAuthenticatedUserGrain : Grain, INonAuthenticatedUser
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

        public async Task<IUser> GetUser()
        {
            var user = GrainFactory.GetGrain<IUser>(_uuid);
            await user.SetName(this.GetPrimaryKeyString());
            return user;
        }
    }
}
