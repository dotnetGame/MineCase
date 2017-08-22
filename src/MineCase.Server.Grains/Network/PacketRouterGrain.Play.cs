using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Server.Game;
using Orleans;

namespace MineCase.Server.Network
{
    internal partial class PacketRouterGrain
    {
        private object DeserializePlayPacket(ref UncompressedPacket packet)
        {
            using (var br = new BinaryReader(new MemoryStream(packet.Data)))
            {
                object innerPacket;
                switch (packet.PacketId)
                {
                    // Teleport Confirm
                    case 0x00:
                        innerPacket = TeleportConfirm.Deserialize(br);
                        break;

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

                    // Position And Look
                    case 0x0F:
                        innerPacket = ServerboundPositionAndLook.Deserialize(br);
                        break;
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X}.");
                }

                if (br.BaseStream.Position != br.BaseStream.Length)
                    throw new InvalidDataException($"Packet data is not fully consumed.");
                return innerPacket;
            }
        }

        private async Task DispatchPacket(TeleportConfirm packet)
        {
            var player = await _user.GetPlayer();
            player.OnTeleportConfirm(packet.TeleportId).Ignore();
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
            _user.KeepAlive(packet.KeepAliveId).Ignore();
            return Task.CompletedTask;
        }

        private async Task DispatchPacket(ServerboundPositionAndLook packet)
        {
            var player = await _user.GetPlayer();
            player.SetPositionAndLook(packet.X, packet.FeetY, packet.Z, packet.Yaw, packet.Pitch, packet.OnGround).Ignore();
        }

        private Task DispatchPacket(UseEntity packet)
        {
            var player = _user;

            // player.UseEntity((uint)packet.Target, (EntityUsage)packet.Type, packet.TargetX.HasValue ?
            //    new Vector3?(new Vector3(packet.TargetX.Value, packet.TargetY.Value, packet.TargetZ.Value)) : null, (EntityInteractHand?)packet.Hand).Ignore();
            return Task.CompletedTask;
        }
    }
}
