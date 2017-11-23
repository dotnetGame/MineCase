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

        private AutoSaveStateComponent _autoSave;
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

        protected override async Task InitializePreLoadComponent()
        {
            var state = new StateComponent<StateHolder>();
            await SetComponent(state);
            state.BeforeWriteState += State_BeforeWriteState;
            state.AfterReadState += State_AfterReadState;
        }

        private Task State_AfterReadState(object sender, EventArgs e)
        {
            _colliders = State.Colliders.ToDictionary(o => o.Object, o => o.Collider);
            return Task.CompletedTask;
        }

        private Task State_BeforeWriteState(object sender, EventArgs e)
        {
            State.Colliders = (from c in _colliders select new CollectablePair { Object = c.Key, Collider = c.Value }).ToList();
            return Task.CompletedTask;
        }

        protected override async Task InitializeComponents()
        {
            _autoSave = new AutoSaveStateComponent(AutoSaveStateComponent.PerMinute);
            await SetComponent(_autoSave);
        }

        private Dictionary<IDependencyObject, Shape> _colliders;

        public Task RegisterCollider(IDependencyObject entity, Shape colliderShape)
        {
            _colliders[entity] = colliderShape;
            MarkDirty();
            return Collision(entity, colliderShape);
        }

        public Task UnregisterCollider(IDependencyObject entity)
        {
            if (_colliders.Remove(entity))
                MarkDirty();
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

        private void MarkDirty()
        {
            _autoSave.IsDirty = true;
        }

        public Task OnGameTick(GameTickArgs e)
        {
            return _autoSave.OnGameTick(this, e);
        }

        internal class CollectablePair
        {
            public IDependencyObject Object { get; set; }

            public Shape Collider { get; set; }
        }

        internal class StateHolder
        {
            public List<CollectablePair> Colliders { get; set; }

            public StateHolder()
            {
            }

            public StateHolder(InitializeStateMark mark)
            {
                Colliders = new List<CollectablePair>();
            }
        }
    }
}
