using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Handshaking;

namespace MineCase.Client.Network.Handshaking
{
    public interface IHandshakingPacketGenerator
    {
        Task Handshake(uint protocolVersion, string serverAddress, ushort serverPort, uint nextState);
    }

    internal class HandshakingHandler : IHandshakingPacketGenerator
    {
        private readonly IPacketSink _packetSink;
        private readonly IPacketRouter _packetRouter;

        public HandshakingHandler(IPacketSink packetSink, IPacketRouter packetRouter)
        {
            _packetSink = packetSink;
            _packetRouter = packetRouter;
        }

        async Task IHandshakingPacketGenerator.Handshake(uint protocolVersion, string serverAddress, ushort serverPort, uint nextState)
        {
            if (nextState != 1 && nextState != 2) throw new ArgumentOutOfRangeException(nameof(nextState));

            await _packetSink.SendPacket(new Handshake
            {
                ProtocolVersion = protocolVersion,
                ServerAddress = serverAddress,
                ServerPort = serverPort,
                NextState = nextState
            });
            _packetRouter.State = nextState == 1 ? SessionState.Status : SessionState.Login;
        }
    }
}
