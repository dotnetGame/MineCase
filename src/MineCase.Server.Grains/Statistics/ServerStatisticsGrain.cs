using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Settings;
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
            Name = "1.15.2"
        };

        public async Task<string> GetDescription()
        {
            var serverSettings = GrainFactory.GetGrain<IServerSettings>(0);
            return (await serverSettings.GetSettings()).Motd;
        }

        public Task<byte[]> GetFavicon()
        {
            return Task.FromResult(Array.Empty<byte>());
        }

        public async Task<uint> GetMaxPlayersCount()
        {
            var serverSettings = GrainFactory.GetGrain<IServerSettings>(0);
            return (await serverSettings.GetSettings()).MaxPlayers;
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
