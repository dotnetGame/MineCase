using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.Network.Handler.Play
{
    public class ServerPlayNetHandler : IServerPlayNetHandler
    {
        private IPacketRouter _clientSession;

        private IClientboundPacketSink _packetSink;

        private IGrainFactory _client;

        public ServerPlayNetHandler(IPacketRouter session, IClientboundPacketSink packetSink, IGrainFactory client)
        {
            _clientSession = session;
            _packetSink = packetSink;
            _client = client;
        }
    }
}
