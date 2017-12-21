using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Network.Play;
using Orleans;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PlayerListComponent : Component<PlayerGrain>, IHandle<PlayerLoggedIn>, IHandle<PlayerListAdd>, IHandle<AskPlayerDescription, PlayerDescription>, IHandle<PlayerListRemove>
    {
        public PlayerListComponent(string name = "playerList")
            : base(name)
        {
        }

        async Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            await SendPlayerListAddPlayer(new[] { AttachedObject });
        }

        async Task IHandle<PlayerListAdd>.Handle(PlayerListAdd message)
        {
            await SendPlayerListAddPlayer(message.Players);
        }

        Task<PlayerDescription> IHandle<AskPlayerDescription, PlayerDescription>.Handle(AskPlayerDescription message)
        {
            return Task.FromResult(new PlayerDescription
            {
                UUID = AttachedObject.GetPrimaryKey(),
                Name = AttachedObject.GetComponent<NameComponent>().Name,
                GameMode = new GameMode { ModeClass = GameMode.Class.Creative },
                Ping = AttachedObject.GetComponent<KeepAliveComponent>().Ping
            });
        }

        Task IHandle<PlayerListRemove>.Handle(PlayerListRemove message)
        {
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .PlayerListItemRemovePlayer((from p in message.Players
                                             select p.GetPrimaryKey()).ToList());
        }

        private async Task SendPlayerListAddPlayer(IReadOnlyList<IPlayer> players)
        {
            var desc = await Task.WhenAll(from p in players
                                          select p.Ask(AskPlayerDescription.Default));
            await AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator()
                .PlayerListItemAddPlayer(desc);
            await AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .PlayerListItemAddPlayer(desc);
        }
    }
}
