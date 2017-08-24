using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
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
        private readonly Dictionary<IUser, UserContext> _users = new Dictionary<IUser, UserContext>();

        private IDisposable _gameTick;
        private DateTime _lastGameTickTime;

        public override async Task OnActivateAsync()
        {
            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(this.GetPrimaryKeyString());
            _lastGameTickTime = DateTime.UtcNow;
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

        public async Task SendChatMessage(string senderName, string message)
        {
            // TODO command parser
            Chat jsonData = Chat.Parse("{\"translate\":\"chat.type.text\",\"with\":[\"" + senderName + "\",\"" + message + "\"]}");
            byte position = 0; // It represents user message in chat box
            foreach (var item in _users.Keys)
            {
                await item.SendChatMessage(jsonData, position);
            }
        }

        public async Task SendChatMessage(string senderName, string receiverName, string message)
        {
            Chat jsonData = Chat.Parse("{\"text\":\"" + senderName + "\",\"text\":\"" + message + "\"}");
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
            var now = DateTime.UtcNow;
            var deltaTime = now - _lastGameTickTime;
            _lastGameTickTime = now;

            await Task.WhenAll(_users.Keys.Select(o => o.OnGameTick(deltaTime)));
        }

        private class UserContext
        {
            public ClientPlayPacketGenerator Generator { get; set; }
        }
    }
}
