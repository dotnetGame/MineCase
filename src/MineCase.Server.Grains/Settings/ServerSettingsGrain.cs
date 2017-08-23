using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Settings
{
    [StatelessWorker]
    internal class ServerSettingsGrain : Grain, IServerSettings
    {
        private ServerSettings _settings;

        // read settings from file
        public override async Task OnActivateAsync()
        {
            string settingsFile = await ReadSettingsAsString("server.json");
            _settings = JsonConvert.DeserializeObject<ServerSettings>(settingsFile);
        }

        // get settings
        public Task<ServerSettings> GetSettigns() => Task.FromResult(_settings);

        // set settings
        public Task SetSettigns(ServerSettings settings)
        {
            _settings = settings;
            return Task.CompletedTask;
        }

        private Task<string> ReadSettingsAsString(string path)
        {
            string result = null;
            try
            {
                result = File.ReadAllText(path);
            }
            catch (IOException e)
            {
                Console.WriteLine("io异常" + e.ToString());
            }

            return Task.FromResult(result);
        }
    }
}
