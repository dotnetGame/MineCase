using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Logging;
using MineCase.Engine;
using MineCase.Protocol;
using MineCase.Protocol.Play;
using MineCase.Serialization;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.World;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Play
{
    internal class ServerboundPacketComponent : Component<PlayerGrain>, IHandle<ServerboundPacketMessage>
    {
        private readonly ConcurrentQueue<object> _deferredPacket = new ConcurrentQueue<object>();
        private readonly ActionBlock<UncompressedPacket> _receivePacket;

        public ServerboundPacketComponent(string name = "serverboundPacket")
            : base(name)
        {
            _receivePacket = new ActionBlock<UncompressedPacket>((Action<UncompressedPacket>)OnReceivePacket);
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

        private async Task OnGameTick(object sender, GameTickArgs e)
        {
            while (_deferredPacket.TryDequeue(out var packet))
                await DispatchPacket((dynamic)packet);
        }

        Task IHandle<ServerboundPacketMessage>.Handle(ServerboundPacketMessage message)
        {
            _receivePacket.Post(message.Packet);
            return Task.CompletedTask;
        }

        private void OnReceivePacket(UncompressedPacket rawPacket)
        {
            var packet = DeserializePlayPacket(rawPacket);
            if (packet != null)
                _deferredPacket.Enqueue(packet);
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
            return AttachedObject.GetComponent<TeleportComponent>().ConfirmTeleport(packet.TeleportId);
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
            return AttachedObject.GetComponent<KeepAliveComponent>().ReceiveResponse(packet.KeepAliveId);
        }

        private async Task DispatchPacket(ServerboundPositionAndLook packet)
        {
            await AttachedObject.GetComponent<EntityWorldPositionComponent>()
                .SetPosition(new EntityWorldPos((float)packet.X, (float)packet.FeetY, (float)packet.Z));
            var lookComponent = AttachedObject.GetComponent<EntityLookComponent>();
            await lookComponent.SetPitch(packet.Pitch);
            await lookComponent.SetYaw(packet.Yaw);
            await AttachedObject.GetComponent<EntityOnGroundComponent>()
                .SetIsOnGround(packet.OnGround);
        }

        private async Task DispatchPacket(PlayerOnGround packet)
        {
            await AttachedObject.GetComponent<EntityOnGroundComponent>()
                .SetIsOnGround(packet.OnGround);
        }

        private async Task DispatchPacket(PlayerPosition packet)
        {
            await AttachedObject.GetComponent<EntityWorldPositionComponent>()
                .SetPosition(new EntityWorldPos((float)packet.X, (float)packet.FeetY, (float)packet.Z));
            await AttachedObject.GetComponent<EntityOnGroundComponent>()
                .SetIsOnGround(packet.OnGround);
        }

        private async Task DispatchPacket(PlayerLook packet)
        {
            var lookComponent = AttachedObject.GetComponent<EntityLookComponent>();
            await lookComponent.SetPitch(packet.Pitch);
            await lookComponent.SetYaw(packet.Yaw);
            await AttachedObject.GetComponent<EntityOnGroundComponent>()
                .SetIsOnGround(packet.OnGround);
        }

        private Task DispatchPacket(UseEntity packet)
        {
            Logger.LogWarning("UseEntity is not implemented.");

            // player.UseEntity((uint)packet.Target, (EntityUsage)packet.Type, packet.TargetX.HasValue ?
            //    new Vector3?(new Vector3(packet.TargetX.Value, packet.TargetY.Value, packet.TargetZ.Value)) : null, (EntityInteractHand?)packet.Hand).Ignore();
            return Task.CompletedTask;
        }

        private Task DispatchPacket(ServerboundHeldItemChange packet)
        {
            return AttachedObject.GetComponent<HeldItemComponent>().SetHeldItemIndex(packet.Slot);
        }

        private async Task DispatchPacket(PlayerDigging packet)
        {
            var face = ConvertDiggingFace(packet.Face);
            var component = AttachedObject.GetComponent<DiggingComponent>();
            switch (packet.Status)
            {
                case PlayerDiggingStatus.StartedDigging:
                    await component.StartDigging(packet.Location, face);
                    break;
                case PlayerDiggingStatus.CancelledDigging:
                    await component.CancelDigging(packet.Location, face);
                    break;
                case PlayerDiggingStatus.FinishedDigging:
                    await component.FinishDigging(packet.Location, face);
                    break;
                default:
                    Logger.LogWarning($"Not implemented digging status: {packet.Status}");
                    break;
            }
        }

        private Task DispatchPacket(EntityAction packet)
        {
            Logger.LogWarning("EntityAction is not implemented.");

            // TODO Set Entity Action
            return Task.CompletedTask;
        }

        private Task DispatchPacket(ServerboundAnimation packet)
        {
            Logger.LogWarning("Animation is not implemented.");

            return Task.CompletedTask;
        }

        private async Task DispatchPacket(PlayerBlockPlacement packet)
        {
            var face = ConvertDiggingFace(packet.Face);
            await AttachedObject.GetComponent<BlockPlacementComponent>()
                .PlaceBlock(packet.Location, (EntityInteractHand)packet.Hand, face, new Vector3(packet.CursorPositionX, packet.CursorPositionY, packet.CursorPositionZ));
        }

        private Task DispatchPacket(UseItem packet)
        {
            return Task.CompletedTask;
        }

        private async Task DispatchPacket(ClickWindow packet)
        {
            try
            {
                await AttachedObject.GetComponent<WindowManagerComponent>()
                    .ClickWindow(packet.WindowId, packet.Slot, ToClickAction(packet.Button, packet.Mode, packet.Slot), packet.ActionNumber, packet.ClickedItem);
            }
            catch
            {
            }
        }

        private Task DispatchPacket(ServerboundCloseWindow packet)
        {
            return AttachedObject.GetComponent<WindowManagerComponent>()
                .CloseWindow(packet.WindowId);
        }

        private PlayerDiggingFace ConvertDiggingFace(Protocol.Play.PlayerDiggingFace face)
        {
            return (PlayerDiggingFace)face;
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
