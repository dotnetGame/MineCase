using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Status;
using MineCase.Server.Network.Status;
using Orleans;

namespace MineCase.Server.Network
{
    internal partial class PacketRouterGrain
    {
        private object DeserializeStatusPacket(ref UncompressedPacket packet)
        {
            using (var br = new BinaryReader(new MemoryStream(packet.Data)))
            {
                switch (packet.PacketId)
                {
                    // Request
                    case 0x00:
                        return Request.Deserialize(br);

                    // Ping
                    case 0x01:
                        return Ping.Deserialize(br);
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X}.");
                }
            }
        }

        private Task DispatchPacket(Request packet)
        {
            var requestGrain = GrainFactory.GetGrain<IRequest>(0);
            requestGrain.DispatchPacket(this.GetPrimaryKey(), packet).Ignore();
            return Task.CompletedTask;
        }

        private Task DispatchPacket(Ping packet)
        {
            var requestGrain = GrainFactory.GetGrain<IPing>(0);
            requestGrain.DispatchPacket(this.GetPrimaryKey(), packet).Ignore();
            return Task.CompletedTask;
        }
    }
}
