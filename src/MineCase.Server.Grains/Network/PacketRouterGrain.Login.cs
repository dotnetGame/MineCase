using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Login;
using MineCase.Serialization;
using MineCase.Server.Network.Login;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Network
{
    internal partial class PacketRouterGrain
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
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
            }

            if (!br.IsCosumed)
                throw new InvalidDataException($"Packet data is not fully consumed.");
            return task;
        }

        private async Task DispatchPacket(LoginStart packet)
        {
            var user = await GrainFactory.GetGrain<INonAuthenticatedUser>(packet.Name).GetUser();
            await user.SetProtocolVersion(_protocolVersion);

            var requestGrain = GrainFactory.GetGrain<ILoginFlow>(this.GetPrimaryKey());
            requestGrain.DispatchPacket(packet).Ignore();
        }
    }
}
