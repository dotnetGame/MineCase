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
        private readonly Dictionary<IPlayer, ClientPlayPacketGenerator> _players = new Dictionary<IPlayer, ClientPlayPacketGenerator>();
        
        public async Task JoinGame(IPlayer player)
        {
            var generator = new ClientPlayPacketGenerator(await player.GetClientPacketSink());
            _players.Add(player, generator);
            await generator.JoinGame()
        }

        public override async Task OnActivateAsync()
        {
            _world = await GrainFactory.GetGrain<IWorldAccessor>(0).GetWorld(this.GetPrimaryKeyString());
        }
    }
}
