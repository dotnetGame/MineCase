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
        private Task DispatchPlayPackets(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            switch (packet.PacketId)
            {
                case 0x00:
                    // return DispatchPacket(Request.Deserialize(ref br));
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
            }
        }
    }
}
