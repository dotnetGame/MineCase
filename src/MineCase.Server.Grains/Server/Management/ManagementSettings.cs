using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Server;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Server.Management
{
    public class ManagementSettings : Grain, IManagementSettings
    {
        private IMinecraftServer _server;
        private bool _whiteListEnforced;
        protected readonly int _maxPlayers;
        private int _viewDistance;
        private GameType _gameType;
        private bool _commandsAllowedForAll;
        private int _playerPingIndex;
    }
}
