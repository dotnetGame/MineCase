using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Settings
{
    [Reentrant]
    internal class ServerSettingsGrain : Grain, IServerSettings
    {
        private ServerSettings _settings;

        // get settings
        public Task<ServerSettings> GetSettigns() => Task.FromResult(_settings);

        // set settings
        public Task SetSettigns(ServerSettings settings)
        {
            _settings = settings;
            return Task.CompletedTask;
        }
    }
}
