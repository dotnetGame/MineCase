using Microsoft.Extensions.Logging;
using MineCase.Engine;
using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Play
{
    internal class ServerboundPacketComponent : Component<PlayerGrain>, IHandle<ServerboundPacketMessage>
    {
        private readonly Queue<object> _deferredPacket = new Queue<object>();

        public ServerboundPacketComponent(string name = "serverboundPacket")
            : base(name)
        {
        }

        protected override Task OnAttached()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick += OnGameTick;
            return base.OnAttached();
        }

        protected override Task OnDetached()
        {
            AttachedObject.GetComponent<GameTickComponent>()
                .Tick -= OnGameTick;
            return base.OnDetached();
        }

        private async Task OnGameTick(object sender, (TimeSpan deltaTime, long worldAge) e)
        {
            while (_deferredPacket.Count != 0)
                await DispatchPacket((dynamic)_deferredPacket.Dequeue());
        }

        Task IHandle<ServerboundPacketMessage>.Handle(ServerboundPacketMessage message)
        {
            var packet = DeserializePlayPacket(message.Packet);
            if (packet != null)
                _deferredPacket.Enqueue(message.Packet);
            return Task.CompletedTask;
        }

        private Task DispatchPacket(object packet)
        {
            throw new NotImplementedException();
        }

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
                    innerPacket = PlayerOnGround.Deserialize(ref br);
                    break;

                // Player Position
                case 0x0E:
                    innerPacket = PlayerPosition.Deserialize(ref br);
                    break;

                // Position And Look
                case 0x0F:
                    innerPacket = ServerboundPositionAndLook.Deserialize(ref br);
                    break;

                // Player Look
                case 0x10:
                    innerPacket = PlayerLook.Deserialize(ref br);
                    break;

                // Player Digging
                case 0x14:
                    innerPacket = PlayerDigging.Deserialize(ref br);
                    break;

                // Entity Action
                case 0x15:
                    innerPacket = EntityAction.Deserialize(ref br);
                    break;

                // Held Item Change
                case 0x1A:
                    innerPacket = ServerboundHeldItemChange.Deserialize(ref br);
                    break;

                // Animation
                case 0x1D:
                    innerPacket = ServerboundAnimation.Deserialize(ref br);
                    break;

                // Player Block Placement
                case 0x1F:
                    innerPacket = PlayerBlockPlacement.Deserialize(ref br);
                    break;

                // Use Item
                case 0x20:
                    innerPacket = UseItem.Deserialize(ref br);
                    break;

                // Click Window
                case 0x08:
                    innerPacket = ClickWindow.Deserialize(ref br);
                    break;

                // Close Window
                case 0x09:
                    innerPacket = ServerboundCloseWindow.Deserialize(ref br);
                    break;
                default:
                    Logger.LogWarning($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
                    return null;
            }

            if (!br.IsCosumed)
                throw new InvalidDataException($"Packet data is not fully consumed.");
            return innerPacket;
        }

        private Task DispatchPacket(TeleportConfirm packet)
        {
            return AttachedObject.OnTeleportConfirm(packet.TeleportId);
        }

        private Task DispatchPacket(ServerboundChatMessage packet)
        {
            /*
            var gameSession = await _user.GetGameSession();
            await gameSession.SendChatMessage(_user, packet.Message);
            */
            return Task.CompletedTask;
        }

        private async Task DispatchPacket(ClientSettings packet)
        {
            await AttachedObject.SetLocalValue(ViewDistanceComponent.ViewDistanceProperty, packet.ViewDistance);
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

        private async Task DispatchPacket(PlayerOnGround packet)
        {
            var player = await _user.GetPlayer();
            await player.SetOnGround(packet.OnGround);
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
            // TODO Set Entity Action
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

        private async Task DispatchPacket(ClickWindow packet)
        {
            try
            {
                var player = await _user.GetPlayer();
                player.ClickWindow(packet.WindowId, packet.Slot, ToClickAction(packet.Button, packet.Mode, packet.Slot), packet.ActionNumber, packet.ClickedItem).Ignore();
            }
            catch
            {
            }
        }

        private async Task DispatchPacket(ServerboundCloseWindow packet)
        {
            var player = await _user.GetPlayer();
            player.CloseWindow(packet.WindowId).Ignore();
        }

        private Game.PlayerDiggingFace ConvertDiggingFace(Protocol.Play.PlayerDiggingFace face)
        {
            return (Game.PlayerDiggingFace)face;
        }

        private ClickAction ToClickAction(byte button, uint mode, short slot)
        {
            switch (mode)
            {
                case 0:
                    switch (button)
                    {
                        case 0:
                            return ClickAction.LeftMouseClick;
                        case 1:
                            return ClickAction.RightMouseClick;
                        default:
                            break;
                    }

                    break;
                case 1:
                    switch (button)
                    {
                        case 0:
                            return ClickAction.LeftMouseClick;
                        case 1:
                            return ClickAction.RightMouseClick;
                        default:
                            break;
                    }

                    break;
                case 2:
                    switch (button)
                    {
                        case 0:
                            return ClickAction.NumberKey1;
                        case 1:
                            return ClickAction.NumberKey2;
                        case 2:
                            return ClickAction.NumberKey3;
                        case 3:
                            return ClickAction.NumberKey4;
                        case 4:
                            return ClickAction.NumberKey5;
                        case 5:
                            return ClickAction.NumberKey6;
                        case 6:
                            return ClickAction.NumberKey7;
                        case 7:
                            return ClickAction.NumberKey8;
                        case 8:
                            return ClickAction.NumberKey9;
                        default:
                            break;
                    }

                    break;
                case 3:
                    switch (button)
                    {
                        case 2:
                            return ClickAction.MiddleClick;
                        default:
                            break;
                    }

                    break;
                case 4:
                    switch (button)
                    {
                        case 0:
                            return slot == -999 ? ClickAction.LeftClickNoting : ClickAction.DropKey;
                        case 1:
                            return slot == -999 ? ClickAction.RightClickNothing : ClickAction.CtrlDropKey;
                        default:
                            break;
                    }

                    break;
                default:
                    break;
            }

            throw new NotSupportedException("This button-mode is not supported");
        }
    }

    [Immutable]
    public sealed class ServerboundPacketMessage : IEntityMessage
    {
        public UncompressedPacket Packet { get; set; }
    }
}
