using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class MobDiscoveryComponent : Component<EntityGrain>, IHandle<DiscoveredByPlayer>, IHandle<BroadcastDiscovered>, IHandle<SpawnEntity>
    {
        public MobDiscoveryComponent(string name = "mobDiscovery")
            : base(name)
        {
        }

        private ClientPlayPacketGenerator GetPlayerPacketGenerator(IPlayer player) =>
            new ClientPlayPacketGenerator(new ForwardToPlayerPacketSink(player, ServiceProvider.GetRequiredService<IPacketPackager>()));

        Task IHandle<DiscoveredByPlayer>.Handle(DiscoveredByPlayer message)
        {
            MobType type = AttachedObject.GetComponent<MobTypeComponent>().MobType;

            // Logger.LogInformation($"Mob spawn, type: {type}");
            return GetPlayerPacketGenerator(message.Player)
                .SpawnMob(AttachedObject.EntityId, AttachedObject.UUID, (byte)type, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, new EntityMetadata.Entity { });
        }

        Task IHandle<BroadcastDiscovered>.Handle(BroadcastDiscovered message)
        {
            MobType type = AttachedObject.GetComponent<MobTypeComponent>().MobType;
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .SpawnMob(AttachedObject.EntityId, AttachedObject.UUID, (byte)type, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, new EntityMetadata.Entity { });
        }

        async Task IHandle<SpawnEntity>.Handle(SpawnEntity message)
        {
            await AttachedObject.SetLocalValue(MobTypeComponent.MobTypeProperty, message.MobType);

            // Logger.LogInformation($"Mob spawn, key: {AttachedObject.GetAddressByPartitionKey()}");
            // Logger.LogInformation($"Mob spawn, type: {message.MobType}");
        }
    }
}
