using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using MineCase.Protocol.Play;
using MineCase.Server.Game.Commands;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network;
using MineCase.Server.User;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities
{
    public interface IPlayer : IEntity, ICommandSender
    {
        Task<string> GetName();

        Task SetName(string name);

        Task<IUser> GetUser();

        Task BindToUser(IUser user);

        Task<PlayerDescription> GetDescription();

        Task<IInventoryWindow> GetInventory();

        Task SendWholeInventory();

        Task SendExperience();

        Task<Slot> Collect(uint collectedEntityId, Slot item);

        Task SendPlayerListAddPlayer(IReadOnlyList<IPlayer> player);

        Task OpenWindow(IWindow window);

        Task NotifyLoggedIn();

        Task SendPositionAndLook();

        Task OnTeleportConfirm(uint teleportId);

        Task SetPosition(double x, double feetY, double z, bool onGround);

        Task SetLook(float yaw, float pitch, bool onGround);

        Task<(float pitch, float yaw)> GetLook();

        Task StartDigging(Position location, PlayerDiggingFace face);

        Task CancelDigging(Position location, PlayerDiggingFace face);

        Task FinishDigging(Position location, PlayerDiggingFace face);

        Task Spawn(Guid uuid, Vector3 position, float pitch, float yaw);

        Task PlaceBlock(Position location, EntityInteractHand hand, PlayerDiggingFace face, Vector3 cursorPosition);

        Task SetHeldItem(short slot);

        Task<Slot> GetInventorySlot(int index);

        Task SetInventorySlot(int index, Slot slot);

        Task<byte> GetWindowId(IWindow window);

        Task SetDraggedSlot(Slot item);

        Task<Slot> GetDraggedSlot();

        Task ClickWindow(byte windowId, short slot, ClickAction clickAction, short actionNumber, Slot clickedItem);

        Task CloseWindow(byte windowId);

        Task SetOnGround(bool state);

        Task TossPickup(Slot slot);
    }
}
