using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Protocol.Play;
using MineCase.Server.Game.Windows;
using MineCase.Server.Network;
using MineCase.Server.User;

namespace MineCase.Server.Game.Entities
{
    public interface IPlayer : IEntity
    {
        Task<string> GetName();

        Task SetName(string name);

        Task<IUser> GetUser();

        Task BindToUser(IUser user);

        Task<PlayerDescription> GetDescription();

        Task<IInventoryWindow> GetInventory();

        Task SendWholeInventory();

        Task SendExperience();

        Task<bool> Collect(uint collectedEntityId, Slot item);

        Task SendPlayerListAddPlayer(IReadOnlyList<IPlayer> player);

        Task NotifyLoggedIn();

        Task SendPositionAndLook();

        Task OnTeleportConfirm(uint teleportId);

        Task SetPosition(double x, double feetY, double z, bool onGround);

        Task SetLook(float yaw, float pitch, bool onGround);

        Task StartDigging(Position location, PlayerDiggingFace face);

        Task CancelDigging(Position location, PlayerDiggingFace face);

        Task FinishDigging(Position location, PlayerDiggingFace face);

        Task Spawn(Guid uuid, Vector3 position, float pitch, float yaw);

        Task PlaceBlock(Position location, EntityInteractHand hand, PlayerDiggingFace face, Vector3 cursorPosition);

        Task SetHeldItem(short slot);

        Task SetOnGround(bool state);
    }
}
