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

        Task IHandle<SpawnMob>.Handle(SpawnMob message)
        {
            AttachedObject.Tell<SpawnEntity>(message);
            AttachedObject.SetLocalValue(MobTypeComponent.MobTypeProperty, message.MobType);
            CompleteSpawn();

            // Logger.LogInformation($"Mob spawn, key: {AttachedObject.GetAddressByPartitionKey()}");
            // Logger.LogInformation($"Mob spawn, type: {message.MobType}");
            return Task.CompletedTask;
        }

        protected override Task SendSpawnPacket(ClientPlayPacketGenerator generator)
        {
            MobType type = AttachedObject.GetComponent<MobTypeComponent>().MobType;
            return generator.SpawnLivingEntity(AttachedObject.EntityId, AttachedObject.UUID, (byte)type, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, new EntityMetadata.Entity { });
        }
    }
}
