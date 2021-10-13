using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MineCase.Engine;
using MineCase.Resources;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;
using MineCase.Server.Persistence.Components;
using MineCase.Server.Server.Level;
using MineCase.Server.Server.Players;
using MineCase.Server.Settings;
using MineCase.Server.User;
using MineCase.Server.World.Level;
using MineCase.World;

namespace MineCase.Server.Server
{
    internal class MinecraftServer : DependencyObject, IMinecraftServer
    {
        // private bool _willStartRecordingMetrics;
        // private bool _debugCommandProfilerDelayStart;
        // private readonly ServerStatus status = new ServerStatus();
        // private string localIp;
        // private int port = -1;
        // private PlayerList playerList;
        // private bool running = true;
        // private bool stopped;
        // private int tickCount;
        // private bool onlineMode;
        // private bool preventProxyConnections;
        // private bool pvp;
        // private bool allowFlight;
        // private string motd;
        // private int playerIdleTimeout;
        // private string singleplayerName;
        // private bool isDemo;
        // private string resourcePack = "";
        // private string resourcePackHash = "";
        // private bool isReady;
        // private long lastOverloadWarning;
        // private long lastServerStatus;
        // protected long nextTickTime;
        // private long delayedTasksMaxNextTickTime;
        // private bool mayHaveDelayedTasks;
        // private bool enforceWhitelist;
        // private float averageTickTime;
        // private string serverId;
        private readonly Dictionary<ResourceKey<MineCase.Server.World.Level.Level>, ServerLevel> levels = new Dictionary<ResourceKey<World.Level.Level>, ServerLevel>();
        private readonly PlayerList playerList = new PlayerList();
        private readonly Dictionary<IUser, UserContext> _users = new Dictionary<IUser, UserContext>();
        private ILogger _logger;

        public MinecraftServer()
        {
        }

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<MinecraftServer>();
        }

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
        }

        public async Task JoinGame(IUser user)
        {
            var sink = await user.GetClientPacketSink();
            var generator = new ClientPlayPacketGenerator(sink);
            var settings = await GrainFactory.GetGrain<IServerSettings>(0).GetSettings();

            _users[user] = new UserContext
            {
                Generator = generator
            };

            await user.JoinGame();
            var player = await user.GetPlayer();
            await generator.JoinGame(
                await player.GetEntityId(),
                await user.GetGameMode(),
                Dimension.Overworld,
                Difficulty.Easy,
                (byte)settings.MaxPlayers,
                await player.GetViewDistance(),
                LevelTypes.Default,
                false);
            await user.NotifyLoggedIn();
            await SendWholePlayersList(user);
        }

        public Task LeaveGame(IUser user)
        {
            _users.Remove(user);
            return BroadcastRemovePlayerFromList(user);
        }

        private async Task BroadcastRemovePlayerFromList(IUser user)
        {
            var players = new List<IPlayer> { await user.GetPlayer() };
            await Task.WhenAll(from u in _users.Keys select u.RemovePlayerList(players));
        }

        public async Task SendWholePlayersList(IUser user)
        {
            var list = await Task.WhenAll(from p in _users.Keys
                                          select p.GetPlayer());

            await user.UpdatePlayerList(list);
        }

        public async Task SendChatMessage(IUser sender, string message)
        {
            var senderName = await sender.GetName();

            // TODO command parser
            // construct name
            Chat jsonData = await CreateStandardChatMessage(senderName, message);
            byte position = 0; // It represents user message in chat box
            foreach (var item in _users.Keys)
            {
                await item.SendChatMessage(jsonData, position);
            }
        }

        public async Task SendChatMessage(IUser sender, IUser receiver, string message)
        {
            var senderName = await sender.GetName();
            var receiverName = await receiver.GetName();

            Chat jsonData = await CreateStandardChatMessage(senderName, message);
            byte position = 0; // It represents user message in chat box
            foreach (var item in _users.Keys)
            {
                if (await item.GetName() == receiverName ||
                    await item.GetName() == senderName)
                    await item.SendChatMessage(jsonData, position);
            }
        }

        private Task<Chat> CreateStandardChatMessage(string name, string message)
        {
            StringComponent nameComponent = new StringComponent(name);
            nameComponent.ClickEvent = new ChatClickEvent(ClickEventType.SuggestCommand, "/msg " + name);
            nameComponent.HoverEvent = new ChatHoverEvent(HoverEventType.ShowEntity, name);
            nameComponent.Insertion = name;

            // construct message
            StringComponent messageComponent = new StringComponent(message);

            // list
            List<ChatComponent> list = new List<ChatComponent>();
            list.Add(nameComponent);
            list.Add(messageComponent);

            Chat jsonData = new Chat(new TranslationComponent("chat.type.text", list));
            return Task.FromResult(jsonData);
        }

        public Task<int> UserNumber()
        {
            return Task.FromResult(_users.Count);
        }

        public Task<bool> UserFull()
        {
            throw new NotImplementedException();
        }

        private class UserContext
        {
            public ClientPlayPacketGenerator Generator { get; set; }
        }
    }
}
