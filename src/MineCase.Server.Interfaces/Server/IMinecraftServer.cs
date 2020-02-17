using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Server
{
    public interface IMinecraftServer : IGrainWithStringKey
    {
        Task<bool> GetOnlineMode();

        Task<int> GetNetworkCompressionThreshold();
    }
}
