using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.User;
using Orleans;

namespace MineCase.Server.Server.Players
{
    public class PlayerList
    {
        private readonly Dictionary<Guid, IUser> _players = new Dictionary<Guid, IUser>();

        // private bool _doWhiteList;
        protected readonly int maxPlayers;

        // private int _viewDistance;
        // private bool allowCheatsForAllPlayers;
        // private static readonly bool ALLOWLOGOUTIVATOR = false;
        // private int sendAllPlayerInfoIn;
        public PlayerList()
        {
            // _viewDistance = 16;
        }

        public void JoinPlayer(IUser user)
        {
            Guid playerId = user.GetPrimaryKey();
            if (!_players.ContainsKey(playerId))
                _players.Add(playerId, user);
        }

        public void LeavePlayer(IUser user)
        {
            Guid playerId = user.GetPrimaryKey();
            if (_players.ContainsKey(playerId))
                _players.Remove(playerId);
        }
    }
}
