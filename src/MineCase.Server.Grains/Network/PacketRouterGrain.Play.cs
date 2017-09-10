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

                // Player On Ground
                case 0x0D:
                    innerPacket = DeferPacket(PlayerOnGround.Deserialize(ref br));
                    break;

                // Player Position
                case 0x0E:
                    innerPacket = DeferPacket(PlayerPosition.Deserialize(ref br));
                    break;

                // Position And Look
                case 0x0F:
                    innerPacket = DeferPacket(ServerboundPositionAndLook.Deserialize(ref br));
                    break;

                // Player Look
                case 0x10:
                    innerPacket = DeferPacket(PlayerLook.Deserialize(ref br));
                    break;

                // Player Digging
                case 0x14:
                    innerPacket = DeferPacket(PlayerDigging.Deserialize(ref br));
                    break;

                // Entity Action
                case 0x15:
                    innerPacket = DeferPacket(EntityAction.Deserialize(ref br));
                    break;

                // Held Item Change
                case 0x1A:
                    innerPacket = DeferPacket(ServerboundHeldItemChange.Deserialize(ref br));
                    break;

                // Animation
                case 0x1D:
                    innerPacket = DeferPacket(ServerboundAnimation.Deserialize(ref br));
                    break;

                // Player Block Placement
                case 0x1F:
                    innerPacket = DeferPacket(PlayerBlockPlacement.Deserialize(ref br));
                    break;

                // Use Item
                case 0x20:
                    innerPacket = DeferPacket(UseItem.Deserialize(ref br));
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

        private Task DispatchPacket(PlayerOnGround packet)
        {
            // TODO set player state
            return Task.CompletedTask;
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

        private async Task DispatchPacket(ServerboundHeldItemChange packet)
        {
            var player = await _user.GetPlayer();
            player.SetHeldItem(packet.Slot).Ignore();
        }

        private async Task DispatchPacket(PlayerDigging packet)
        {
            var face = ConvertDiggingFace(packet.Face);
            var player = await _user.GetPlayer();
            switch (packet.Status)
            {
                case PlayerDiggingStatus.StartedDigging:
                    player.StartDigging(packet.Location, face).Ignore();
                    break;
                case PlayerDiggingStatus.CancelledDigging:
                    player.CancelDigging(packet.Location, face).Ignore();
                    break;
                case PlayerDiggingStatus.FinishedDigging:
                    player.FinishDigging(packet.Location, face).Ignore();
                    break;
                case PlayerDiggingStatus.DropItemStack:
                    break;
                case PlayerDiggingStatus.DropItem:
                    break;
                case PlayerDiggingStatus.ShootArrowOrFinishEating:
                    break;
                case PlayerDiggingStatus.SwapItemInHand:
                    break;
                default:
                    break;
            }
        }

        private Task DispatchPacket(EntityAction packet)
        {
            // TODO set player action
            return Task.CompletedTask;
        }

        private Task DispatchPacket(ServerboundAnimation packet)
        {
            return Task.CompletedTask;
        }

        private async Task DispatchPacket(PlayerBlockPlacement packet)
        {
            var face = ConvertDiggingFace(packet.Face);
            var player = await _user.GetPlayer();
            player.PlaceBlock(packet.Location, (EntityInteractHand)packet.Hand, face, new Vector3(packet.CursorPositionX, packet.CursorPositionY, packet.CursorPositionZ)).Ignore();
        }

        private Task DispatchPacket(UseItem packet)
        {
            return Task.CompletedTask;
        }

        private Game.PlayerDiggingFace ConvertDiggingFace(Protocol.Play.PlayerDiggingFace face)
        {
            return (Game.PlayerDiggingFace)face;
        }
    }
}
