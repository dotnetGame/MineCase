using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.User
{
    public interface INonAuthenticatedUser : IGrainWithStringKey
    {
        Task<IUser> GetUser();
    }
}
