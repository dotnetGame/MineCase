using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.User;

namespace MineCase.Server.Server.Players
{
    public class PlayerList
    {
        private readonly List<UserGrain> players = new List<UserGrain>();
        private readonly Dictionary<Guid, UserGrain> playersByUUID = new Dictionary<Guid, UserGrain>();
        private bool doWhiteList;
        protected readonly int maxPlayers;
        private int viewDistance;
        private bool allowCheatsForAllPlayers;
        private static readonly bool ALLOWLOGOUTIVATOR = false;
        private int sendAllPlayerInfoIn;
        private readonly List<UserGrain> playersView = new List<UserGrain>();
    }
}
