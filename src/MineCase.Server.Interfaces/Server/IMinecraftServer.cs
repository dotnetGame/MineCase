using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Server
{
    public interface IMinecraftServer : IGrainWithStringKey
    {
        Task JoinGame(IUser player);

        Task LeaveGame(IUser player);

        Task SendChatMessage(IUser sender, String message);

        Task SendChatMessage(IUser sender, IUser receiver, String messages);

        Task<int> UserNumber();

        Task<bool> UserFull();
    }
}
