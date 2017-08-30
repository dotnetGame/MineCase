using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.Server.Game;
using Orleans;

namespace MineCase.Server.Network
{
    internal partial class PacketRouterGrain
    {
        private object DeserializePlayPacket(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            object innerPacket;
            switch (packet.PacketId)
            {
                // Teleport Confirm
                case 0x00:
                    innerPacket = TeleportConfirm.Deserialize(ref br);
                    break;

                // Chat Message
                case 0x03:
                    innerPacket = ServerboundChatMessage.Deserialize(ref br);
                    break;

                // Client Settings
                case 0x05:
                    innerPacket = ClientSettings.Deserialize(ref br);
                    break;

                // Plugin Message
                case 0x0A:
                    innerPacket = ServerboundPluginMessage.Deserialize(ref br);
                    break;

                // Keep Alive
                case 0x0C:
                    innerPacket = ServerboundKeepAlive.Deserialize(ref br);
                    break;

                // Position And Look
                case 0x0F:
                    innerPacket = DeferPacket(ServerboundPositionAndLook.Deserialize(ref br));
                    break;

                // Player Position
                case 0x0E:
                    innerPacket = DeferPacket(PlayerPosition.Deserialize(ref br));
                    break;

                // Player Look
                case 0x10:
                    innerPacket = DeferPacket(PlayerLook.Deserialize(ref br));
                    break;

                // Server Animation
                case 0x1D:
                    innerPacket = DeferPacket(ServerAnimation.Deserialize(ref br));
                    break;

                // Player Digging
                case 0x14:
                    innerPacket = DeferPacket(PlayerDigging.Deserialize(ref br));
                    break;

                // Held Item Change
                case 0x1A:
                    innerPacket = DeferPacket(ServerboundHeldItemChange.Deserialize(ref br));
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
            }

            if (!br.IsCosumed)
                throw new InvalidDataException($"Packet data is not fully consumed.");
            return innerPacket;
        }

        private async Task DispatchPacket(TeleportConfirm packet)
        {
            var player = await _user.GetPlayer();
            player.OnTeleportConfirm(packet.TeleportId).Ignore();
        }

        private async Task DispatchPacket(ServerboundChatMessage packet)
        {
            var gameSession = await _user.GetGameSession();
            await gameSession.SendChatMessage(_user, packet.Message);
        }

        private Task DispatchPacket(ClientSettings packet)
        {
            _user.SetViewDistance(packet.ViewDistance);
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
            player.SetPosition(packet.X, packet.FeetY, packet.Z, packet.OnGround).Ignore();
            player.SetLook(packet.Yaw, packet.Pitch, packet.OnGround).Ignore();
        }

        private async Task DispatchPacket(PlayerPosition packet)
        {
            var player = await _user.GetPlayer();
            player.SetPosition(packet.X, packet.FeetY, packet.Z, packet.OnGround).Ignore();
        }

        private async Task DispatchPacket(PlayerLook packet)
        {
            var player = await _user.GetPlayer();
            player.SetLook(packet.Yaw, packet.Pitch, packet.OnGround).Ignore();
        }

        private Task DispatchPacket(UseEntity packet)
        {
            var player = _user;

            // player.UseEntity((uint)packet.Target, (EntityUsage)packet.Type, packet.TargetX.HasValue ?
            //    new Vector3?(new Vector3(packet.TargetX.Value, packet.TargetY.Value, packet.TargetZ.Value)) : null, (EntityInteractHand?)packet.Hand).Ignore();
            return Task.CompletedTask;
        }

        private Task DispatchPacket(ServerboundHeldItemChange packet)
        {
            return Task.CompletedTask;
        }

        private async Task DispatchPacket(ServerAnimation packet)
        {
            var player = await _user.GetPlayer();
            var handState = await player.OnSwingHand(packet.HandState);

            // TODO:check enum to enum.
            await player.SendClientAnimation((ClientAnimationID)handState);
        }

        private Task DispatchPacket(PlayerDigging packet)
        {
            // TODO:update world state.
            return Task.CompletedTask;
        }
    }
}
