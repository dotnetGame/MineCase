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
                object innerPacket;
                switch (packet.PacketId)
                {
                    // Login Start
                    case 0x00:
                        innerPacket = LoginStart.Deserialize(br);
                        break;
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X}.");
                }
                if (br.BaseStream.Position != br.BaseStream.Length)
                    throw new InvalidDataException($"Packet data is not fully consumed.");
                return innerPacket;
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
