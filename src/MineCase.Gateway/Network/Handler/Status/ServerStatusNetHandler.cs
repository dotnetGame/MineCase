using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol.Status.Client;
using MineCase.Protocol.Protocol.Status.Server;

namespace MineCase.Gateway.Network.Handler.Status
{
    public class ServerStatusNetHandler : IServerStatusNetHandler
    {
        private ClientSession _clientSession;

        public ServerStatusNetHandler(ClientSession session)
        {
            _clientSession = session;
        }

        public Task ProcessPing(Ping packetIn)
        {
            var pong = new Pong();
            pong.Payload = packetIn.Payload;
            return _clientSession.SendPacket(pong);
        }

        public Task ProcessRequest(Request packetIn)
        {
            var reponse = new Response();
            reponse.JsonResponse = @"
{""version"": {
        ""name"": ""1.15.2"",
        ""protocol"": 578
    }, ""players"": {
        ""max"": 100,
        ""online"": 0,
        ""sample"": []
    }, ""description"": { ""text"": ""Hello MineCase""},
""favicon"": null
}";
            return _clientSession.SendPacket(reponse);
        }
    }
}
