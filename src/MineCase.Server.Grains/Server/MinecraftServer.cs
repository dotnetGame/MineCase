using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Server
{
    public class MinecraftServer : Grain, IMinecraftServer
    {
        public Task<int> GetNetworkCompressionThreshold()
        {
            return Task.FromResult(-1);
        }

        public Task<bool> GetOnlineMode()
        {
            return Task.FromResult(false);
        }
    }
}
