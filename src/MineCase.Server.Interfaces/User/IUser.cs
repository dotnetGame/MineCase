using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Server.User
{
    public interface IUser : IGrainWithStringKey, IGrain
    {
        Task<string> GetName();

        Task Login(Guid sessionId);

        Task Logout();

        Task SetPosition(EntityWorldPos pos);
    }
}
