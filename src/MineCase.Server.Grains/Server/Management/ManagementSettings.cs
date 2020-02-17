using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Server.Server;

namespace MineCase.Server.Server.Management
{
    public abstract class ManagementSettings
    {
        private IMinecraftServer _server;

        private readonly List<ServerPlayerEntity> players = new List<>();
        private readonly Map<UUID, ServerPlayerEntity> uuidToPlayerMap = Maps.newHashMap();
        private readonly BanList bannedPlayers = new BanList(FILE_PLAYERBANS);
        private readonly IPBanList bannedIPs = new IPBanList(FILE_IPBANS);
        private readonly OpList ops = new OpList(FILE_OPS);
        private readonly WhiteList whiteListedPlayers = new WhiteList(FILE_WHITELIST);
        private readonly Map<UUID, ServerStatisticsManager> playerStatFiles = Maps.newHashMap();
        private readonly Map<UUID, PlayerAdvancements> advancements = Maps.newHashMap();
        private IPlayerFileData playerDataManager;
        private bool whiteListEnforced;
        protected readonly int maxPlayers;
        private int viewDistance;
        private GameType gameType;
        private bool commandsAllowedForAll;
        private int playerPingIndex;
        private readonly List<ServerPlayerEntity> playersView = java.util.Collections.unmodifiableList(players);

    }
}
