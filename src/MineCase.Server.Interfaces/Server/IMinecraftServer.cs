using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Server.MultiPlayer;
using Orleans;

namespace MineCase.Server.Server
{
    public interface IMinecraftServer : IGrainWithStringKey
    {
        Task<bool> GetOnlineMode();

        Task<int> GetNetworkCompressionThreshold();

        Task UserJoin(IUser user);

        Task UserLeave();

        Task<int> UserNumber();
    }
}
