using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol.Status.Client;
using MineCase.Protocol.Protocol.Status.Server;
using Orleans;

namespace MineCase.Server.Network.Handler.Status
{
    public class ServerStatusNetHandler : IServerStatusNetHandler
    {
        private IPacketRouter _clientSession;

        private IClientboundPacketSink _packetSink;

        private IGrainFactory _client;

        public ServerStatusNetHandler(IPacketRouter session, IClientboundPacketSink packetSink, IGrainFactory client)
        {
            _clientSession = session;
            _packetSink = packetSink;
            _client = client;
        }

        public Task ProcessPing(Ping packetIn)
        {
            var pong = new Pong();
            pong.Payload = packetIn.Payload;
            return _packetSink.SendPacket(pong);
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
    }, ""description"": { ""text"": ""Hello MineCase""}
}";
            return _packetSink.SendPacket(reponse);
        }
    }
}
