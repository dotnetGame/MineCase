using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MineCase.Engine;
using MineCase.Graphics;
using MineCase.Server.Components;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Network;
using MineCase.Server.Network.Play;

namespace MineCase.Server.Game.Entities.Components
{
    internal class PickupDiscoveryComponent : EntityDiscoveryComponentBase<PickupGrain>, IHandle<DestroyEntity>, IHandle<SpawnEntity>
    {
        public PickupDiscoveryComponent(string name = "pickupDiscovery")
            : base(name)
        {
        }

        protected override Task SendSpawnPacket(ClientPlayPacketGenerator generator)
        {
            return generator.SpawnObject(AttachedObject.EntityId, AttachedObject.UUID, 2, AttachedObject.Position, AttachedObject.Pitch, AttachedObject.Yaw, 0);
        }

        Task IHandle<DestroyEntity>.Handle(DestroyEntity message)
        {
            return AttachedObject.GetComponent<ChunkEventBroadcastComponent>().GetGenerator()
                .DestroyEntities(new[] { AttachedObject.EntityId });
        }

        Task IHandle<SpawnEntity>.Handle(SpawnEntity message)
        {
            var pos = message.Position;
            var bb = BoundingBox.Item();
            var box = new Cuboid(new Point3d(pos.X, pos.Z, pos.Y), new Size(bb.X, bb.Y, bb.Z));
            AttachedObject.SetLocalValue(ColliderComponent.ColliderShapeProperty, box);
            CompleteSpawn();

            return Task.CompletedTask;
        }
    }
}
