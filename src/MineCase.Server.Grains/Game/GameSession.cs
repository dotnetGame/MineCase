using MineCase.Server.World;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.User;
using MineCase.Server.Network.Play;
using System.Linq;

namespace MineCase.Server.Game
{
    class GameSession : Grain, IGameSession
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
            await generator.JoinGame((await user.GetPlayer()).GetEntityId(), new GameMode { ModeClass = GameMode.Class.Survival },
                 Dimension.Overworld, Difficulty.Easy, 10, LevelTypes.Default, false);
            await user.NotifyLoggedIn();
        }

        public Task LeaveGame(IUser player)
        {
            _users.Remove(player);
            return Task.CompletedTask;
        }

        private async Task OnGameTick(object state)
        {
            var now = DateTime.UtcNow;
            var deltaTime = now - _lastGameTickTime;
            _lastGameTickTime = now;

            await Task.WhenAll(_users.Keys.Select(o => o.OnGameTick(deltaTime)));
        }

        class UserContext
        {
            public ClientPlayPacketGenerator Generator { get; set; }
        }
    }
}
