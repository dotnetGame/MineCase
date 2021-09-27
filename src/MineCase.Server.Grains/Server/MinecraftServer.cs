using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Resources;
using MineCase.Server.Network;
using MineCase.Server.Server.Level;
using MineCase.Server.Server.Players;
using MineCase.Server.World.Level;

namespace MineCase.Server.Server
{
    public class MinecraftServer
    {
        private bool _willStartRecordingMetrics;
        private bool _debugCommandProfilerDelayStart;
        private readonly ServerStatus status = new ServerStatus();
        private string localIp;
        private int port = -1;
        private readonly Dictionary<ResourceKey<MineCase.Server.World.Level.Level>, ServerLevel> levels;
        private PlayerList playerList;
        private bool running = true;
        private bool stopped;
        private int tickCount;
        private bool onlineMode;
        private bool preventProxyConnections;
        private bool pvp;
        private bool allowFlight;
        private string motd;
        private int playerIdleTimeout;
        private string singleplayerName;
        private bool isDemo;
        private string resourcePack = "";
        private string resourcePackHash = "";
        private bool isReady;
        private long lastOverloadWarning;
        private long lastServerStatus;
        protected long nextTickTime;
        private long delayedTasksMaxNextTickTime;
        private bool mayHaveDelayedTasks;
        private bool enforceWhitelist;
        private float averageTickTime;
        private string serverId;
    }
}
