using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Entities
{
    internal abstract class EntityGrain : Grain, IEntity
    {
        protected IWorld World { get; private set; }

        protected uint EntityId { get; private set; }

        protected Vector3 Position { get; set; }

        protected Guid UUID { get; set; }

        public Task<Vector3> GetPosition() => Task.FromResult(Position);

        public override Task OnActivateAsync()
        {
            var keys = this.GetWorldAndEntityId();
            World = GrainFactory.GetGrain<IWorld>(keys.worldKey);
            EntityId = keys.entityId;

            return Task.CompletedTask;
        }

        public Task SetPosition(Vector3 position)
        {
            Position = position;
            return Task.CompletedTask;
        }

        Task<(int x, int y, int z)> IEntity.GetChunkPosition() => Task.FromResult(GetChunkPosition());

        protected (int x, int y, int z) GetChunkPosition() => ((int)(Position.X / 16), (int)(Position.Y / 16), (int)(Position.Z / 16));

        protected ClientPlayPacketGenerator GetBroadcastGenerator(int chunkX, int chunkZ)
        {
            return new ClientPlayPacketGenerator(GrainFactory.GetGrain<IChunkTrackingHub>(World.MakeChunkTrackingHubKey(chunkX, chunkZ)));
        }

        protected ClientPlayPacketGenerator GetBroadcastGenerator()
        {
            var chunk = GetChunkPosition();
            return GetBroadcastGenerator(chunk.x, chunk.z);
        }
    }
}
