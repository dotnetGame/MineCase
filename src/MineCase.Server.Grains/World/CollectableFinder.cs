using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Graphics;
using MineCase.Server.Components;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class CollectableFinder : Grain, ICollectableFinder
    {
        private IWorld _world;

        private List<(Cuboid box, ICollectableFinder finder)> _neighborFinders;

        public static readonly (int x, int z)[] CrossCoords = new[]
        {
            (0, 0), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1)
        };

        /*
        private List<ICollectable> _collectables;
        */
        public override Task OnActivateAsync()
        {
            _world = GrainFactory.GetGrain<IWorld>(this.GetWorldAndChunkWorldPos().worldKey);
            var selfPos = this.GetChunkWorldPos();
            _neighborFinders = new List<(Cuboid box, ICollectableFinder finder)>();
            foreach (var crossCoord in CrossCoords)
            {
                var newPos = new ChunkWorldPos(selfPos.X + crossCoord.x, selfPos.Z + crossCoord.z);
                var shape = new Cuboid(new Point3d(newPos.X * 16, newPos.Z * 16, 0), new Size(16, 16, 256));
                _neighborFinders.Add((shape, GrainFactory.GetPartitionGrain<ICollectableFinder>(_world, newPos)));
            }

            _colliders = new Dictionary<IDependencyObject, Shape>();
            return base.OnActivateAsync();
        }

        private Dictionary<IDependencyObject, Shape> _colliders;

        public Task RegisterCollider(IDependencyObject entity, Shape colliderShape)
        {
            _colliders[entity] = colliderShape;
            return Collision(entity, colliderShape);
        }

        public Task UnregisterCollider(IDependencyObject entity)
        {
            _colliders.Remove(entity);
            return Task.CompletedTask;
        }

        private async Task Collision(IDependencyObject entity, Shape colliderShape)
        {
            var result = (await Task.WhenAll(from finder in _neighborFinders
                                             where colliderShape.CollideWith(finder.box)
                                             select finder.finder.CollisionInChunk(colliderShape)))
                                             .SelectMany(o => o).ToList();
            result.Remove(entity);
            if (result.Any())
                await entity.Tell(new CollisionWith { Entities = result });
        }

        public async Task SpawnPickup(Vector3 position, Immutable<Slot[]> slots)
        {
            foreach (var slot in slots.Value)
            {
                var pickup = GrainFactory.GetGrain<IPickup>(Guid.NewGuid());
                await pickup.Tell(new SpawnEntity
                {
                    World = _world,
                    EntityId = await _world.NewEntityId(),
                    Position = position
                });
                await pickup.Tell(new SetSlot { Slot = slot });
            }
        }

        public Task<IReadOnlyCollection<IDependencyObject>> CollisionInChunk(Shape colliderShape)
        {
            List<IDependencyObject> result = null;
            foreach (var collider in _colliders)
            {
                if (collider.Value.CollideWith(colliderShape))
                {
                    if (result == null)
                        result = new List<IDependencyObject>();
                    result.Add(collider.Key);
                }
            }

            return Task.FromResult((IReadOnlyCollection<IDependencyObject>)result ?? Array.Empty<IDependencyObject>());
        }
    }
}
