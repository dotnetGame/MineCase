using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MineCase.Protocol.Play;
using MineCase.Server.Network.Play;
using MineCase.Server.User;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game
{
    internal class GameSession : Grain, IGameSession
    {
        private IWorld _world;
        private IChunkSender _chunkSender;
        private readonly Dictionary<IUser, UserContext> _users = new Dictionary<IUser, UserContext>();

        private IDisposable _gameTick;
        private DateTime _lastGameTickTime;

        private HashSet<ITickable> _tickables;

        private readonly Commands.CommandMap _commandMap = new Commands.CommandMap();

        public override async Task OnActivateAsync()
        {
            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(this.GetPrimaryKeyString());
            _chunkSender = GrainFactory.GetGrain<IChunkSender>(this.GetPrimaryKeyString());
            _lastGameTickTime = DateTime.UtcNow;
            _tickables = new HashSet<ITickable>();
            _gameTick = RegisterTimer(OnGameTick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(50));
        }

        public Task<IEnumerable<IUser>> GetUsers()
        {
            return Task.FromResult((IEnumerable<IUser>)_users.Keys);
        }

        public async Task<IUser> FindUserByName(string name)
        {
            foreach (var user in _users)
            {
                if (await user.Key.GetName() == name)
                {
                    return user.Key;
                }
            }

            return null;
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
                (await user.GetPlayer()).GetEntityId(),
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

            if (!string.IsNullOrWhiteSpace(message))
            {
                var command = message.Trim();
                if (command[0] == '/')
                {
                    if (!await _commandMap.Dispatch(await sender.GetPlayer(), message))
                    {
                        await SendSystemMessage(sender, $"试图执行指令 \"{command}\" 时被拒绝，请检查是否具有足够的权限以及指令语法是否正确");
                    }

                    return;
                }
            }

            // construct name
            var jsonData = await CreateStandardChatMessage(senderName, message);
            const byte position = 0; // It represents user message in chat box
            foreach (var item in _users.Keys)
            {
                await item.SendChatMessage(jsonData, position);
            }
        }

        public async Task SendChatMessage(IUser sender, IUser receiver, string message)
        {
            var senderName = await sender.GetName();
            var receiverName = await receiver.GetName();

            var jsonData = await CreateStandardChatMessage(senderName, message);
            const byte position = 0; // It represents user message in chat box
            foreach (var item in _users.Keys)
            {
                if (await item.GetName() == receiverName ||
                    await item.GetName() == senderName)
                    await item.SendChatMessage(jsonData, position);
            }
        }

        public async Task SendSystemMessage(IUser receiver, string message)
        {
            var receiverName = await receiver.GetName();

            var jsonData = await CreateStandardSystemMessage(message);
            const byte position = 1; // It represents user message as system message
            await receiver.SendChatMessage(jsonData, position);
        }

        private async Task OnGameTick(object state)
        {
            var now = DateTime.UtcNow;
            var deltaTime = now - _lastGameTickTime;
            _lastGameTickTime = now;

            var worldAge = await _world.GetAge();
            await _world.OnGameTick(deltaTime);
            await Task.WhenAll(from u in _users.Keys
                               select u.OnGameTick(deltaTime));
            await Task.WhenAll(from u in _tickables
                               select u.OnGameTick(deltaTime, worldAge));
        }

        private Task<Chat> CreateStandardChatMessage(string name, string message)
        {
            var nameComponent = new StringComponent(name)
            {
                ClickEvent = new ChatClickEvent(ClickEventType.SuggestCommand, "/msg " + name),
                HoverEvent = new ChatHoverEvent(HoverEventType.ShowEntity, name),
                Insertion = name
            };

            // construct message
            var messageComponent = new StringComponent(message);

            // list
            var list = new List<ChatComponent>
            {
                nameComponent, messageComponent
            };

            var jsonData = new Chat(new TranslationComponent("chat.type.text", list));
            return Task.FromResult(jsonData);
        }

        private Task<Chat> CreateStandardSystemMessage(string message)
        {
            var jsonData = new Chat(new StringComponent(message));
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
