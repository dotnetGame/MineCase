using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Status;
using MineCase.Serialization;
using Orleans;

namespace MineCase.Gateway.Network
{
    /// <summary>
    /// Packet router grain used in status requests and response.
    /// </summary>
    internal partial class PacketRouter
    {
        private Task DispatchStatusPackets(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            switch (packet.PacketId)
            {
                // Request
                case 0x00:
                    return DispatchPacket(Request.Deserialize(ref br));

                // Ping
                case 0x01:
                    return DispatchPacket(Ping.Deserialize(ref br));
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
            }
        }

        private Task DispatchPacket(Request packet)
        {
            return Task.CompletedTask;
        }

        private Task DispatchPacket(Ping packet)
        {
            return Task.CompletedTask;
        }
    }
}
