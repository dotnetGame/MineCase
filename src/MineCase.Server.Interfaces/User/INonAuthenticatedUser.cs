using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.User
{
    public interface INonAuthenticatedUser : IGrainWithStringKey
    {
        Task<IUser> GetUser();

        Task<uint> GetProtocolVersion();

        Task SetProtocolVersion(uint version);
    }
}
