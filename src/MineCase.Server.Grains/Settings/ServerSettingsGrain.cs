using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace MineCase.Server.Settings
{
    [StatelessWorker]
    internal class ServerSettingsGrain : Grain, IServerSettings
    {
        private ServerSettings _settings;
        private readonly ILogger _logger;

        public ServerSettingsGrain(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ServerSettingsGrain>();
        }

        // read settings from file
        public override async Task OnActivateAsync()
        {
            string settingsFile = await ReadSettingsAsString("server.json");
            try
            {
                _settings = JsonConvert.DeserializeObject<ServerSettings>(settingsFile);
            }
            catch (Exception e)
            {
                _logger.LogError(default(EventId), e, e.Message);
            }
        }

        // get settings
        public Task<ServerSettings> GetSettings() => Task.FromResult(_settings);

        // set settings
        public Task SetSettings(ServerSettings settings)
        {
            _settings = settings;
            return Task.CompletedTask;
        }

        private async Task<string> ReadSettingsAsString(string path)
        {
            string result = null;

            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                using (StreamReader sr = new StreamReader(fs))
                {
                    result = await sr.ReadToEndAsync();
                }
            }
            catch (IOException e)
            {
                _logger.LogError(default(EventId), e, e.Message);
            }

            return result;
        }
    }
}
