using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Entities
{
    internal class PickupGrain : EntityGrain, IPickup, ICollectable
    {
        private EntityMetadata.Pickup _metadata;

        public async Task CollectBy(IPlayer player)
        {
            if (await player.Collect(EntityId, _metadata.Item))
            {
                var chunkPos = GetChunkPosition();
                await GrainFactory.GetGrain<IEntityFinder>(World.MakeEntityFinderKey(chunkPos.x, chunkPos.z)).Unregister(this);
                await GetBroadcastGenerator().DestroyEntities(new[] { EntityId });
                DeactivateOnIdle();
            }
        }

        public override Task OnActivateAsync()
        {
            _metadata = new EntityMetadata.Pickup();
            return base.OnActivateAsync();
        }

        public async Task SetItem(Slot item)
        {
            _metadata.Item = item;
            await GetBroadcastGenerator().EntityMetadata(EntityId, _metadata);
        }

        public async Task Spawn(Guid uuid, Vector3 position)
        {
            UUID = uuid;
            await SetPosition(position);
            await GetBroadcastGenerator().SpawnObject(EntityId, uuid, 2, position, 0, 0, 0);

            var chunkPos = GetChunkPosition();
            await GrainFactory.GetGrain<IEntityFinder>(World.MakeEntityFinderKey(chunkPos.x, chunkPos.z)).Register(this);
        }
    }
}
