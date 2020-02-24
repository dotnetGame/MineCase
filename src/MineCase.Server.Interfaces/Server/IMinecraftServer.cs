using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server.MultiPlayer;
using Orleans;

namespace MineCase.Game.Server
{
    public interface IMinecraftServer : IGrainWithIntegerKey
    {
        Task<bool> GetOnlineMode();

        Task<int> GetNetworkCompressionThreshold();

        Task UserJoin(IUser user);

        Task UserLeave();

        Task<int> UserNumber();
    }
}
