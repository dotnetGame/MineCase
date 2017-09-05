using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MineCase.Protocol.Play;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IGameSession : IGrainWithStringKey
    {
        Task JoinGame(IUser player);

        Task LeaveGame(IUser player);

        Task SendChatMessage(IUser sender, String message);

        Task SendChatMessage(IUser sender, IUser receiver, String messages);
    }
}
