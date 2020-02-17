using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Gateway.Network.Handler.Play
{
    public class ServerPlayNetHandler : IServerPlayNetHandler
    {
        private ClientSession _clientSession;

        private IGrainFactory _client;

        public ServerPlayNetHandler(ClientSession session, IGrainFactory client)
        {
            _clientSession = session;
            _client = client;
        }
    }
}
