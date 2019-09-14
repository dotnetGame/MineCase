using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Core;
using MineCase.Protocol.Status;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Status
{
    [StatelessWorker]
    [Reentrant]
    internal class RequestGrain : Grain, IRequest
    {
        private static readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        public async Task DispatchPacket(Guid sessionId, Request packet)
        {
            var serverInfo = new ServerInfo
            {
                Version = new ServerVersion
                {
                    Protocol = Protocol.Protocol.Version,
                    Name = "1.14.4"
                },
                Players = new ServerInfo.PlayersInfo
                {
                    Max = 30,
                    Online = 0,
                    Sample = new ServerInfo.PlayerInfo[0]
                },
                Description = new ServerInfo.DescriptionInfo
                {
                    Text = "MineCase Server"
                },

                // Favicon = "data:image/png;base64," + Convert.ToBase64String(await serverStatGrain.GetFavicon())
            };
            var response = new Response
            {
                JsonResponse = JsonConvert.SerializeObject(serverInfo, Formatting.Indented, _jsonSettings)
            };
            await GrainFactory.GetGrain<IClientboundPacketSink>(sessionId).SendPacket(response);
        }

        private class ServerInfo
        {
            public ServerVersion Version { get; set; }

            public PlayersInfo Players { get; set; }

            public DescriptionInfo Description { get; set; }

            // public string Favicon { get; set; }
            public class PlayerInfo
            {
                public string Name { get; set; }

                public Guid Id { get; set; }
            }

            public class PlayersInfo
            {
                public uint Max { get; set; }

                public uint Online { get; set; }

                public PlayerInfo[] Sample { get; set; }
            }

            public class DescriptionInfo
            {
                public string Text { get; set; }
            }
        }
    }
}
