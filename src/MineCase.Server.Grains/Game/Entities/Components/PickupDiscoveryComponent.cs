using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PickupDiscoveryComponent : Component<PickupGrain>, IHandle<DiscoveredByPlayer>, IHandle<BroadcasstDiscovered>
    {
        public PickupDiscoveryComponent(string name = "pickupDiscovery")
            : base(name)
        {
        }

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        Task IHandle<DiscoveredByPlayer>.Handle(DiscoveredByPlayer message)
        {
            return GetPlayerPacketGenerator(message.Player)
                .SpawnObject(AttachedObject.EntityId, AttachedObject.UUID, 0, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, 0);
        }

        Task IHandle<BroadcasstDiscovered>.Handle(BroadcasstDiscovered message)
        {
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .SpawnObject(AttachedObject.EntityId, AttachedObject.UUID, 0, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, 0);
        }
    }
}
