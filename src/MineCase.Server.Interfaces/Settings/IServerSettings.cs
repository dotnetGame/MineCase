using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Settings
{
    public interface IServerSettings : IGrainWithIntegerKey
    {
        // get settings
        Task<ServerSettings> GetSettings();

        // set settings
        Task SetSettings(ServerSettings settings);
    }
}
