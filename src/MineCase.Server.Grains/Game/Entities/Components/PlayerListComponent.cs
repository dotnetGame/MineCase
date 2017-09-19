﻿using System;
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
    internal class PlayerListComponent : Component<PlayerGrain>, IHandle<PlayerLoggedIn>, IHandle<AskPlayerDescription, PlayerDescription>
    {
        public PlayerListComponent(string name = "playerList")
            : base(name)
        {
        }

        async Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            await SendPlayerListAddPlayer(new[] { AttachedObject });
        }

        Task<PlayerDescription> IHandle<AskPlayerDescription, PlayerDescription>.Handle(AskPlayerDescription message)
        {
            return Task.FromResult(new PlayerDescription
            {
                UUID = AttachedObject.GetPrimaryKey(),
                Name = AttachedObject.GetComponent<NameComponent>().Name,
                GameMode = new GameMode { ModeClass = GameMode.Class.Survival },
                Ping = AttachedObject.GetComponent<KeepAliveComponent>().Ping
            });
        }

        private async Task SendPlayerListAddPlayer(IReadOnlyList<IPlayer> players)
        {
            var desc = await Task.WhenAll(from p in players
                                          select p.Ask(AskPlayerDescription.Default));
            await AttachedObject.GetComponent<ClientboundPacketComponent>().GetGenerator()
                .PlayerListItemAddPlayer(desc);
        }
    }
}
