using MineCase.Server.World;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.User;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game
{
    class GameSession : Grain, IGameSession
    {
        private IWorld _world;
        private readonly Dictionary<IUser, PlayerContext> _players = new Dictionary<IUser, PlayerContext>();

        public override async Task OnActivateAsync()
        {
            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(this.GetPrimaryKeyString());
        }

        public async Task JoinGame(IUser user)
        {
            var sink = await user.GetClientPacketSink();
            var generator = new ClientPlayPacketGenerator(sink);

            _players[user] = new PlayerContext
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
            _players.Remove(player);
            return Task.CompletedTask;
        }

        class PlayerContext
        {
            public ClientPlayPacketGenerator Generator { get; set; }
        }
    }
}
