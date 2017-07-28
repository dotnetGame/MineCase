using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Statistics
{
    public class ServerVersion
    {
        public string Name { get; set; }
        public uint Protocol { get; set; }
    }

    public interface IServerStatistics : IGrainWithIntegerKey
    {
        Task<ServerVersion> GetVersion();
        Task<uint> GetMaxPlayersCount();
        Task<uint> GetOnlinePlayersCount();
        Task<string> GetDescription();
        Task<byte[]> GetFavicon();
    }
}
