using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.User
{
    [Reentrant]
    internal class NonAuthenticatedUserGrain : INonAuthenticatedUser
    {
        public uint ProtocolVersion { get; set; }

        /*
        public async Task<IUser> GetUser()
        {
            var user = GrainFactory.GetGrain<IUser>(State.UUID);
            await user.SetName(this.GetPrimaryKeyString());
            await user.SetProtocolVersion(State.ProtocolVersion);
            await WriteStateAsync();
            return user;
        }
        */

        public Task<uint> GetProtocolVersion()
        {
            return Task.FromResult(ProtocolVersion);
        }

        public Task SetProtocolVersion(uint version)
        {
            ProtocolVersion = version;
            return Task.CompletedTask;
        }
    }
}
