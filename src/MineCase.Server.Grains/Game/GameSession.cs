using MineCase.Server.World;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Player;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game
{
    class GameSession : Grain, IGameSession
    {
        private IWorld _world;
        private readonly Dictionary<IPlayer, PlayerContext> _players = new Dictionary<IPlayer, PlayerContext>();

        public override async Task OnActivateAsync()
        {
            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(this.GetPrimaryKeyString());
        }

        public async Task JoinGame(IPlayer player)
        {
            var sink = await player.GetClientPacketSink();
            var generator = new ClientPlayPacketGenerator(sink);

            _players[player] = new PlayerContext
            {
                Generator = generator
            };

            await generator.JoinGame(await _world.AttachEntity(player), new GameMode { ModeClass = GameMode.Class.Survival },
                 Dimension.Overworld, Difficulty.Easy, 10, LevelTypes.Default, false);
        }

        public Task LeaveGame(IPlayer player)
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
