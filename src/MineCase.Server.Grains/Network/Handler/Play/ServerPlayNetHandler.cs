using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Server.MultiPlayer;
using Orleans;

namespace MineCase.Server.Network.Handler.Play
{
    public class ServerPlayNetHandler : IServerPlayNetHandler
    {
        private IPacketRouter _clientSession;

        private IClientboundPacketSink _packetSink;

        private IGrainFactory _client;

        private IUser _user;

        public ServerPlayNetHandler(IPacketRouter session, IClientboundPacketSink packetSink, IGrainFactory client, IUser user)
        {
            _clientSession = session;
            _packetSink = packetSink;
            _client = client;
            _user = user;
        }
    }
}
