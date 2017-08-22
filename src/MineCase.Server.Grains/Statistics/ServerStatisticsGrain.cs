using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Statistics
{
    [Reentrant]
    internal class ServerStatisticsGrain : Grain, IServerStatistics
    {
        private readonly ServerVersion _version = new ServerVersion
        {
            Protocol = Protocol.Protocol.Version,
            Name = "1.12"
        };

        public Task<string> GetDescription()
        {
            return Task.FromResult("Hello Minecase");
        }

        public Task<byte[]> GetFavicon()
        {
            return Task.FromResult(Array.Empty<byte>());
        }

        public Task<uint> GetMaxPlayersCount()
        {
            return Task.FromResult(100u);
        }

        public Task<uint> GetOnlinePlayersCount()
        {
            return Task.FromResult(0u);
        }

        public Task<ServerVersion> GetVersion()
        {
            return Task.FromResult(_version);
        }
    }
}
