using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Login;
using MineCase.Serialization;
using MineCase.Server.Game;
using MineCase.Server.User;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Network
{
    /// <summary>
    /// Packet router grain used in login stage.
    /// </summary>
    internal partial class PacketRouterGrain
    {
        private Task DispatchLoginPackets(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            Task task = null;
            switch (packet.PacketId)
            {
                // Login Start
                case 0x00:
                    task = DispatchPacket(LoginStart.Deserialize(ref br));
                    break;

                // Encryption Response
                case 0x01:
                    // task = DispatchPacket(EncryptionResponse.Deserialize(ref br));
                    break;

                // Login Plugin Response
                case 0x02:
                    // task = DispatchPacket(EncryptionResponse.Deserialize(ref br));
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

            // _userName = packet.Name;

            // var user = GrainFactory.GetGrain<INonAuthenticatedUser>(packet.Name);

            // await user.SetProtocolVersion(_protocolVersion);

            // var requestGrain = GrainFactory.GetGrain<ILoginFlow>(this.GetPrimaryKey());

            // requestGrain.DispatchPacket(packet).Ignore();
            var user = GrainFactory.GetGrain<IUser>(packet.Name);
            await user.Login(this.GetPrimaryKey());
            await user.SetPosition(new EntityWorldPos { X = 0.0f, Y = 0.0f, Z = 0.0f });
        }

        // private Task DispatchPacket(EncryptionResponse packet)
        // {
        //    var requestGrain = GrainFactory.GetGrain<ILoginFlow>(this.GetPrimaryKey());
        //    requestGrain.DispatchPacket(packet).Ignore();
        //    return Task.CompletedTask;
        // }
    }
}
