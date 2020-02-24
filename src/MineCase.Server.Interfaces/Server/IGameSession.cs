using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game.Server.MultiPlayer;
using MineCase.Util.Math;
using Orleans;

namespace MineCase.Game.Server
{
    public interface IGameSession : IGrainWithGuidKey
    {
        Task UserEnter(IUser user);

        Task UserLeave(IUser user);

        Task SetPlayerPosition(Guid playerId, EntityPos pos);
    }
}
