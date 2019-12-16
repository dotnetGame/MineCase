using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Game.Entity
{
    public class Player : EntityBase, IPlayer
    {
        public GameMode GameMode { get; set; }

        public string UserName { get; set; }

        public uint Ping { get; set; }

        public Task<GameMode> GetGamemode()
        {
            return Task.FromResult(GameMode);
        }

        public Task<string> GetUserName()
        {
            return Task.FromResult(UserName);
        }

        public Task<uint> GetPing()
        {
            return Task.FromResult(Ping);
        }
    }
}
