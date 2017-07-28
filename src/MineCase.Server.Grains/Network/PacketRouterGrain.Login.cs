using MineCase.Protocol;
using MineCase.Protocol.Login;
using MineCase.Server.Network.Login;
using Orleans;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network
{
    partial class PacketRouterGrain
    {
        private object DeserializeLoginPacket(ref UncompressedPacket packet)
        {
            using (var br = new BinaryReader(new MemoryStream(packet.Data)))
            {
                switch (packet.PacketId)
                {
                    // Login Start
                    case 0x00:
                        return LoginStart.Deserialize(br);
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X}.");
                }
            }
        }

        private Task DispatchPacket(LoginStart packet)
        {
            var requestGrain = GrainFactory.GetGrain<ILoginFlow>(this.GetPrimaryKey());
            requestGrain.DispatchPacket(packet).Ignore();
            return Task.CompletedTask;
        }
    }
}
