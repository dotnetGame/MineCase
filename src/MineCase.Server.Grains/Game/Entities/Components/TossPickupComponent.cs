using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.World;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Entities.Components
{
    internal class TossPickupComponent : Component<EntityGrain>, IHandle<TossPickup>
    {
        public TossPickupComponent(string name = "tossPickup")
            : base(name)
        {
        }

        async Task IHandle<TossPickup>.Handle(TossPickup message)
        {
            var position = AttachedObject.Position;
            var world = AttachedObject.World;
            var yaw = AttachedObject.Yaw;

            var x = -Math.Cos(0) * Math.Sin(yaw);
            var y = -Math.Sin(0);
            var z = Math.Cos(0) * Math.Cos(yaw);
            var look = new Vector3((float)x, (float)y, (float)z);

            // 产生 Pickup
            var finder = GrainFactory.GetPartitionGrain<ICollectableFinder>(world, position.ToChunkWorldPos());
            await finder.SpawnPickup(position + look, message.Slots.AsImmutable());
        }
    }
}
