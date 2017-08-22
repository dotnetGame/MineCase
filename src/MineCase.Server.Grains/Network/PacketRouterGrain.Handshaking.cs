using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Handshaking;
using MineCase.Server.Network.Handshaking;
using Orleans;

namespace MineCase.Server.Network
{
    internal partial class PacketRouterGrain
    {
        private object DeserializeHandshakingPacket(ref UncompressedPacket packet)
        {
            using (var br = new BinaryReader(new MemoryStream(packet.Data)))
            {
                switch (packet.PacketId)
                {
                    // Handshake
                    case 0x00:
                        return Handshake.Deserialize(br);
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X}.");
                }
            }
        }

        private Task DispatchPacket(Handshake packet)
        {
            if (packet.NextState == 1)
                _state = SessionState.Status;
            else if (packet.NextState == 2)
                _state = SessionState.Login;
            else
                throw new InvalidOperationException();

            _protocolVersion = packet.ProtocolVersion;
            return Task.CompletedTask;
        }
    }
}
