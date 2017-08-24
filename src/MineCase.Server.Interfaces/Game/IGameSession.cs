using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Protocol.Play;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IGameSession : IGrainWithStringKey
    {
        Task JoinGame(IUser player);

        Task LeaveGame(IUser player);

        Task SendChatMessage(string senderName, ServerboundChatMessage packet);

        Task SendChatMessage(string senderName, string receiverName, ServerboundChatMessage packet);
    }
}
