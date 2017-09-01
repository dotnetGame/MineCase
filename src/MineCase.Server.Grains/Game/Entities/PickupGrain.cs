using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using Orleans;

namespace MineCase.Server.Game.Entities
{
    internal class PickupGrain : EntityGrain, IPickup
    {
        private EntityMetadata.Pickup _metadata;

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
            Position = position;
            await GetBroadcastGenerator().SpawnObject(EntityId, uuid, 2, position, 0, 0, 0);
        }
    }
}
