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
    internal class MobDiscoveryComponent : EntityDiscoveryComponentBase<EntityGrain>, IHandle<SpawnMob>
    {
        public MobDiscoveryComponent(string name = "mobDiscovery")
            : base(name)
        {
        }

        async Task IHandle<SpawnMob>.Handle(SpawnMob message)
        {
            await AttachedObject.Tell<SpawnEntity>(message);
            await AttachedObject.SetLocalValue(MobTypeComponent.MobTypeProperty, message.MobType);
            CompleteSpawn();

            // Logger.LogInformation($"Mob spawn, key: {AttachedObject.GetAddressByPartitionKey()}");
            // Logger.LogInformation($"Mob spawn, type: {message.MobType}");
        }

        protected override Task SendSpawnPacket(ClientPlayPacketGenerator generator)
        {
            MobType type = AttachedObject.GetComponent<MobTypeComponent>().MobType;
            return generator.SpawnMob(AttachedObject.EntityId, AttachedObject.UUID, (byte)type, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, new EntityMetadata.Entity { });
        }
    }
}
