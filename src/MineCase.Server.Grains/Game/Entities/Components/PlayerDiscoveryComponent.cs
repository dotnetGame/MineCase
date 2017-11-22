using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PlayerDiscoveryComponent : EntityDiscoveryComponentBase<PlayerGrain>, IHandle<PlayerLoggedIn>
    {
        public PlayerDiscoveryComponent(string name = "playerDiscovery")
            : base(name)
        {
        }

        protected override Task SendSpawnPacket(ClientPlayPacketGenerator generator)
        {
            return Task.CompletedTask;
        }

        Task IHandle<PlayerLoggedIn>.Handle(PlayerLoggedIn message)
        {
            CompleteSpawn();
            return Task.CompletedTask;
        }
    }
}
