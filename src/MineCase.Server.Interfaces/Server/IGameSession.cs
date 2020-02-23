using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Server.MultiPlayer;
using Orleans;

namespace MineCase.Server.Server
{
    public interface IGameSession : IGrainWithGuidKey
    {
        Task UserEnter(IUser user);

        Task UserLeave(IUser user);
    }
}
