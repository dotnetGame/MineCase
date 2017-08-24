using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IGameSession : IGrainWithStringKey
    {
        Task JoinGame(IUser player);

        Task LeaveGame(IUser player);

        Task SendChatMessageToAll(string senderName, Chat jsonData, byte position);

        Task SendChatMessage(string name, Chat jsonData, byte position);
    }
}
