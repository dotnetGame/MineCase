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
            throw new NotImplementedException();
        }
    }
}
