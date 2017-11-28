using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Graphics;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    public interface ICollectableFinder : IAddressByPartition
    {
        Task RegisterCollider(IDependencyObject entity, Shape colliderShape);

        Task UnregisterCollider(IDependencyObject entity);

        Task<IReadOnlyCollection<IDependencyObject>> CollisionInChunk(Shape colliderShape);

        Task SpawnPickup(Vector3 position, Immutable<Slot[]> slots);
    }
}
