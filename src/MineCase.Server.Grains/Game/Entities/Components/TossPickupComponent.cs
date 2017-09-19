using System;
using System.Collections.Generic;
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

            // 产生 Pickup
            var finder = GrainFactory.GetPartitionGrain<ICollectableFinder>(world, position.ToChunkWorldPos());
            await finder.SpawnPickup(position, message.Slots.AsImmutable());
        }
    }
}
