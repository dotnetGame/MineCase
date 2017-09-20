using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Entities.Components;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Entities
{
    internal abstract class EntityGrain : DependencyObject, IEntity
    {
        public Guid UUID => this.GetPrimaryKey();

        public uint EntityId => GetValue(EntityIdComponent.EntityIdProperty);

        public EntityWorldPos Position => GetValue(EntityWorldPositionComponent.EntityWorldPositionProperty);

        public IWorld World => GetValue(WorldComponent.WorldProperty);

        public float Pitch => GetValue(EntityLookComponent.PitchProperty);

        public float Yaw => GetValue(EntityLookComponent.YawProperty);

        protected override async Task InitializeComponents()
        {
            await SetComponent(new EntityIdComponent());
            await SetComponent(new WorldComponent());
            await SetComponent(new EntityWorldPositionComponent());
            await SetComponent(new EntityLookComponent());
            await SetComponent(new AddressByPartitionKeyComponent());
            await SetComponent(new ChunkEventBroadcastComponent());
            await SetComponent(new GameTickComponent());
        }

        Task<uint> IEntity.GetEntityId() =>
            Task.FromResult(EntityId);

        Task<EntityWorldPos> IEntity.GetPosition() =>
            Task.FromResult(Position);

        Task<IWorld> IEntity.GetWorld() =>
            Task.FromResult(World);

        Task<float> IEntity.GetYaw() =>
            Task.FromResult(Yaw);
    }
}
