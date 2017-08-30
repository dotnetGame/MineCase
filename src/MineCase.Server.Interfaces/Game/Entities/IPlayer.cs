using System.Collections.Generic;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Windows;
using MineCase.Server.User;

namespace MineCase.Server.Game.Entities
{
    public interface IPlayer : IEntity
    {
        Task<string> GetName();

        Task SetName(string name);

        Task BindToUser(IUser user);

        Task<PlayerDescription> GetDescription();

        Task<IInventoryWindow> GetInventory();

        Task SendWholeInventory();

        Task SendExperience();

        Task SendPlayerListAddPlayer(IReadOnlyList<IPlayer> player);

        Task NotifyLoggedIn();

        Task SendPositionAndLook();

        Task OnTeleportConfirm(uint teleportId);

        Task SetPosition(double x, double feetY, double z, bool onGround);

        Task SetLook(float yaw, float pitch, bool onGround);

        Task<(int x, int y, int z)> GetChunkPosition();

        Task<SwingHandState> OnSwingHand(SwingHandState handState);

        Task SendClientAnimation(ClientAnimationID animationID);
    }
}
