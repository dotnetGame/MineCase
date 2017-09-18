using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Network.Play;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using MineCase.Engine;
using MineCase.Server.Components;

namespace MineCase.Server.Game.Entities
{
    internal abstract class EntityGrain : DependencyObject, IEntity
    {
        protected override async Task InitializeComponents()
        {
            await SetComponent(new WorldComponent());
            await SetComponent(new EntityWorldPositionComponent());
            await SetComponent(new AddressByPartitionKeyComponent());
            await SetComponent(new GameTickComponent());
        }

        Task<EntityWorldPos> IEntity.GetPosition()
            => Task.FromResult(this.GetEntityWorldPosition());

        Task<IWorld> IEntity.GetWorld()
            => Task.FromResult(this.GetWorld());
    }
}
