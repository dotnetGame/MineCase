using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Play
{
    class ClientPlayPacketGenerator
    {
        private readonly IClientboundPacketSink _sink;

        public ClientPlayPacketGenerator(IClientboundPacketSink sink)
        {
            _sink = sink;
        }

        public Task JoinGame(uint eid, byte gameMode, int dimension, byte difficulty, byte maxPlayers, string levelType, bool reducedDebugInfo)
        {
            return _sink.SendPacket(new )
        }
    }
}
