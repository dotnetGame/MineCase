using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entity
{
    public interface IPlayer : IEntityBase
    {
        Task<GameMode> GetGamemode();

        Task<string> GetUserName();

        Task<uint> GetPing();
    }
}
