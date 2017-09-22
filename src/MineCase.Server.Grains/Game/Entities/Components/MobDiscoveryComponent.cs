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
    internal class MobDiscoveryComponent : Component<EntityGrain>, IHandle<DiscoveredByPlayer>, IHandle<BroadcastDiscovered>
    {
        public MobDiscoveryComponent(string name = "mobDiscovery")
            : base(name)
        {
        }

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        Task IHandle<DiscoveredByPlayer>.Handle(DiscoveredByPlayer message)
        {
            return GetPlayerPacketGenerator(message.Player)
                .SpawnMob(AttachedObject.EntityId, AttachedObject.UUID, 0, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, new EntityMetadata.Entity { });
        }

        Task IHandle<BroadcastDiscovered>.Handle(BroadcastDiscovered message)
        {
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .SpawnMob(AttachedObject.EntityId, AttachedObject.UUID, 0, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, new EntityMetadata.Entity { });
        }
    }
}
