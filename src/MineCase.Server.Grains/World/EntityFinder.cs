using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    internal class EntityFinder : Grain, IEntityFinder
    {
        private IWorld _world;
        private List<IEntity> _entities;

        public override Task OnActivateAsync()
        {
            _world = GrainFactory.GetGrain<IWorld>(this.GetWorldAndChunkPosition().worldKey);
            _entities = new List<IEntity>();
            return base.OnActivateAsync();
        }

        public Task<IReadOnlyCollection<IEntity>> Collision(IEntity entity)
        {
            return CollisionInChunk(entity);
        }

        public async Task<IReadOnlyCollection<ICollectable>> CollisionCollectable(IEntity entity)
        {
            var collection = await CollisionInChunk(entity);
            List<ICollectable> result = new List<ICollectable>();
            foreach (IEntity eachEntity in collection)
            {
                if (eachEntity is ICollectable)
                {
                    result.Add((ICollectable)eachEntity);
                }
            }

            return result;
        }

        public async Task<IReadOnlyCollection<ICreature>> CollisionCreature(IEntity entity)
        {
            var collection = await CollisionInChunk(entity);
            List<ICreature> result = new List<ICreature>();
            foreach (IEntity eachEntity in collection)
            {
                if (eachEntity is ICreature)
                {
                    result.Add((ICreature)eachEntity);
                }
            }

            return result;
        }

        public Task<IReadOnlyCollection<IEntity>> CollisionInChunk(IEntity entity)
        {
            return Task.FromResult<IReadOnlyCollection<IEntity>>(_entities);
        }

        public Task Register(IEntity entity)
        {
            if (!_entities.Contains(entity))
                _entities.Add(entity);

            return Task.CompletedTask;
        }

        public Task Unregister(IEntity entity)
        {
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SpawnPickup(Position location, Immutable<Slot[]> slots)
        {
            foreach (var slot in slots.Value)
            {
                var pickup = GrainFactory.GetGrain<IPickup>(_world.MakeEntityKey(await _world.NewEntityId()));
                await _world.AttachEntity(pickup);
                await pickup.Spawn(
                    Guid.NewGuid(),
                    new Vector3(location.X + 0.5f, location.Y + 0.5f, location.Z + 0.5f));
                await pickup.SetItem(slot);
                pickup.Register().Ignore();
            }
        }
    }
}
