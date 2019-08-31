using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Login;
using MineCase.Serialization;
using Orleans;

namespace MineCase.Gateway.Network
{
    /// <summary>
    /// Packet router grain used in login stage.
    /// </summary>
    internal partial class PacketRouter
    {
        private Task DispatchLoginPackets(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            Task task;
            switch (packet.PacketId)
            {
                // Login Start
                case 0x00:
                    task = DispatchPacket(LoginStart.Deserialize(ref br));
                    break;

                // Encryption Response
                case 0x01:
                    task = DispatchPacket(EncryptionResponse.Deserialize(ref br));
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
            }

            if (!br.IsCosumed)
                throw new InvalidDataException($"Packet data is not fully consumed.");
            return task;
        }

        private Task DispatchPacket(LoginStart packet)
        {
            return Task.CompletedTask;
        }

        private Task DispatchPacket(EncryptionResponse packet)
        {
            return Task.CompletedTask;
        }
    }
}
