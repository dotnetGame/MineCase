using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Game.Entity
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid UUID { get; set; }

        public uint EntityId { get; set; }

        public EntityWorldPos Position { get; set; }

        public IWorld World { get; set; }

        public float Pitch { get; set; }

        public float HeadYaw { get; set; }

        public float Yaw { get; set; }

        public Task<uint> GetEntityId()
        {
            return Task.FromResult(EntityId);
        }

        public Task<float> GetHeadYaw()
        {
            return Task.FromResult(HeadYaw);
        }

        public Task<float> GetPitch()
        {
            return Task.FromResult(Pitch);
        }

        public Task<EntityWorldPos> GetPosition()
        {
            return Task.FromResult(Position);
        }

        public Task<Guid> GetUUID()
        {
            return Task.FromResult(UUID);
        }

        public Task<IWorld> GetWorld()
        {
            return Task.FromResult(World);
        }

        public Task<float> GetYaw()
        {
            return Task.FromResult(Yaw);
        }
    }
}
