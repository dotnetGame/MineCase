using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PlayerDiscoveryComponent : Component<PlayerGrain>, IHandle<DiscoveredByPlayer>
    {
        public PlayerDiscoveryComponent(string name = "playerDiscovery")
            : base(name)
        {
        }

        Task IHandle<DiscoveredByPlayer>.Handle(DiscoveredByPlayer message)
        {
            /*
            AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .SpawnPlayer*/
            return Task.CompletedTask;
        }
    }
}
