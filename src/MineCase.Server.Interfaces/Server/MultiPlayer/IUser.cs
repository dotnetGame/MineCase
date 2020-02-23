using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Server.MultiPlayer
{
    public interface IUser : IGrainWithGuidKey
    {
        Task SetSession(IGameSession session);

        Task<IGameSession> GetSession();

        Task SetName(string name);

        Task<string> GetGame();
    }
}
