using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Server.MultiPlayer
{
    public class UserState
    {
        public string Name { get; set; }
    }

    public class User : Grain<UserState>, IUser
    {
        private IGameSession _gameSession;

        public Task SetSession(IGameSession session)
        {
            _gameSession = session;
            return Task.CompletedTask;
        }

        public Task<IGameSession> GetSession()
        {
            return Task.FromResult(_gameSession);
        }

        public Task SetName(string name)
        {
            State.Name = name;
            return Task.CompletedTask;
        }

        public Task<string> GetGame()
        {
            return Task.FromResult(State.Name);
        }
    }
}
