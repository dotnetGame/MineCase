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
using MineCase.Server.Persistence;
using MineCase.Server.Persistence.Components;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [PersistTableName("collectableFinder")]
    [Reentrant]
    internal class CollectableFinder : AddressByPartitionGrain, ICollectableFinder
    {
        public static readonly (int x, int z)[] CrossCoords = new[]
        {
            (0, 0), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1)
        };

        private List<(Cuboid box, ICollectableFinder finder)> _neighborFinders;

        private StateHolder State => GetValue(StateComponent<StateHolder>.StateProperty);

        public override async Task OnActivateAsync()
        {
            await base.OnActivateAsync();
            _neighborFinders = new List<(Cuboid box, ICollectableFinder finder)>();
            foreach (var crossCoord in CrossCoords)
            {
                var newPos = new ChunkWorldPos(ChunkWorldPos.X + crossCoord.x, ChunkWorldPos.Z + crossCoord.z);
                var shape = new Cuboid(new Point3d(newPos.X * 16, newPos.Z * 16, 0), new Size(16, 16, 256));
                _neighborFinders.Add((shape, GrainFactory.GetPartitionGrain<ICollectableFinder>(World, newPos)));
            }
        }

        protected override void InitializePreLoadComponent()
        {
            var state = new StateComponent<StateHolder>();
            SetComponent(state);
        }

        protected override void InitializeComponents()
        {
            SetComponent(new PeriodicSaveStateComponent(TimeSpan.FromMinutes(1)));
        }

        public Task RegisterCollider(IDependencyObject entity, Shape colliderShape)
        {
            State.Colliders[entity] = colliderShape;
            MarkDirty();
            return Collision(entity, colliderShape);
        }

        public Task UnregisterCollider(IDependencyObject entity)
        {
            if (State.Colliders.Remove(entity))
                MarkDirty();
            return Task.CompletedTask;
        }

        private async Task Collision(IDependencyObject entity, Shape colliderShape)
        {
            var result = new HashSet<IDependencyObject>((await Task.WhenAll(from finder in _neighborFinders
                                                                            where colliderShape.CollideWith(finder.box)
                                                                            select finder.finder.CollisionInChunk(colliderShape)))
                                                                            .SelectMany(o => o));
            result.Remove(entity);
            if (result.Count != 0)
                entity.InvokeOneWay(e => e.Tell(new CollisionWith { Entities = result }));
        }

        public async Task SpawnPickup(Vector3 position, Immutable<Slot[]> slots)
        {
            foreach (var slot in slots.Value)
            {
                var pickup = GrainFactory.GetGrain<IPickup>(Guid.NewGuid());
                await pickup.Tell(new SpawnEntity
                {
                    World = World,
                    EntityId = await World.NewEntityId(),
                    Position = position
                });
                await pickup.Tell(new SetSlot { Slot = slot });
            }
        }

        public Task<IReadOnlyCollection<IDependencyObject>> CollisionInChunk(Shape colliderShape)
        {
            List<IDependencyObject> result = null;
            foreach (var collider in State.Colliders)
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

        private void MarkDirty()
        {
            ValueStorage.IsDirty = true;
        }

        public class StateHolder
        {
            public Dictionary<IDependencyObject, Shape> Colliders { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                Colliders = new Dictionary<IDependencyObject, Shape>();
            }
        }
    }
}
