using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Statistics
{
    public interface IServerStatistics : IGrainWithIntegerKey
    {
        Task<ServerVersion> GetVersion();

        Task<uint> GetMaxPlayersCount();

        Task<uint> GetOnlinePlayersCount();

        Task<string> GetDescription();

        Task<byte[]> GetFavicon();
    }
}
