using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Settings
{
    public interface IServerSettings : IGrainWithIntegerKey
    {
        // get settings
        Task<ServerSettings> GetSettigns();

        // set settings
        Task SetSettigns(ServerSettings settings);
    }
}
