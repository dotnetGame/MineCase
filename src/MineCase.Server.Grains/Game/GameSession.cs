using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MineCase.Protocol.Play;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using MineCase.Server.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game
{
    [Reentrant]
    internal class GameSession : Grain, IGameSession
    {
        private IWorld _world;
        private IChunkSender _chunkSender;
        private readonly Dictionary<IUser, UserContext> _users = new Dictionary<IUser, UserContext>();

        private IDisposable _gameTick;
        private DateTime _lastGameTickTime;

        private HashSet<ITickable> _tickables;

        private ILogger _logger;

        public override async Task OnActivateAsync()
        {
            _logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<GameSession>();
            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(this.GetPrimaryKeyString());
            _chunkSender = GrainFactory.GetGrain<IChunkSender>(this.GetPrimaryKeyString());
            _lastGameTickTime = DateTime.UtcNow;
            _tickables = new HashSet<ITickable>();
            _gameTick = RegisterTimer(OnGameTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(50));
        }

        public async Task JoinGame(IUser user)
        {
            var sink = await user.GetClientPacketSink();
            var generator = new ClientPlayPacketGenerator(sink);

            _users[user] = new UserContext
            {
                Generator = generator
            };

            await user.JoinGame();
            await generator.JoinGame(
                await (await user.GetPlayer()).GetEntityId(),
                new GameMode { ModeClass = GameMode.Class.Survival },
                Dimension.Overworld,
                Difficulty.Easy,
                10,
                LevelTypes.Default,
                false);
            await user.NotifyLoggedIn();
        }

        public Task LeaveGame(IUser player)
        {
            _users.Remove(player);
            return Task.CompletedTask;
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

        private async Task OnGameTick(object state)
        {
            try
            {
                var now = DateTime.UtcNow;
                var deltaTime = now - _lastGameTickTime;
                _lastGameTickTime = now;

                var worldTime = await _world.GetTime();

                if (worldTime.WorldAge % 20 == 0)
                {
                    await Task.WhenAll(from u in _users.Values
                                       select u.Generator.TimeUpdate(worldTime.WorldAge, worldTime.TimeOfDay));
                }

                await _world.OnGameTick(deltaTime);
                await Task.WhenAll(from u in _users.Keys
                                   select u.OnGameTick(deltaTime, worldTime.WorldAge));
                await Task.WhenAll(from u in _tickables
                                   select u.OnGameTick(deltaTime, worldTime.WorldAge));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
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

        public Task Subscribe(ITickable tickable)
        {
            _tickables.Add(tickable);
            return Task.CompletedTask;
        }

        public Task Unsubscribe(ITickable tickable)
        {
            _tickables.Remove(tickable);
            return Task.CompletedTask;
        }

        private class UserContext
        {
            public ClientPlayPacketGenerator Generator { get; set; }
        }
    }
}
