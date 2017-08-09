using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Server.Game;
using Orleans;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network
{
    partial class PacketRouterGrain
    {
        private object DeserializePlayPacket(ref UncompressedPacket packet)
        {
            using (var br = new BinaryReader(new MemoryStream(packet.Data)))
            {
                object innerPacket;
                switch (packet.PacketId)
                {
                    // Client Settings
                    case 0x05:
                        innerPacket = ClientSettings.Deserialize(br);
                        break;
                    // Plugin Message
                    case 0x0A:
                        innerPacket = ServerboundPluginMessage.Deserialize(br);
                        break;
                    // Keep Alive
                    case 0x0C:
                        innerPacket = ServerboundKeepAlive.Deserialize(br);
                        break;
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X}.");
                }
                if (br.BaseStream.Position != br.BaseStream.Length)
                    throw new InvalidDataException($"Packet data is not fully consumed.");
                return innerPacket;
            }
        }

        private Task DispatchPacket(ClientSettings packet)
        {
            return Task.CompletedTask;
        }

        private Task DispatchPacket(ServerboundPluginMessage packet)
        {
            return Task.CompletedTask;
        }

        private Task DispatchPacket(ServerboundKeepAlive packet)
        {
            _player.KeepAlive(packet.KeepAliveId).Ignore();
            return Task.CompletedTask;
        }

        private Task DispatchPacket(UseEntity packet)
        {
            var player = _player;
            player.UseEntity((uint)packet.Target, (EntityUsage)packet.Type, packet.TargetX.HasValue ?
                new Vector3?(new Vector3(packet.TargetX.Value, packet.TargetY.Value, packet.TargetZ.Value)) : null, (EntityInteractHand?)packet.Hand).Ignore();
            return Task.CompletedTask;
        }
    }
}
