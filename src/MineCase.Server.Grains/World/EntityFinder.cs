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
        private List<IPlayer> _playerEntities;
        private List<ICollectable> _collectableEntities;
        private List<ICreature> _creatureEntities;

        public override Task OnActivateAsync()
        {
            _world = GrainFactory.GetGrain<IWorld>(this.GetWorldAndChunkPosition().worldKey);
            _playerEntities = new List<IPlayer>();
            _collectableEntities = new List<ICollectable>();
            _creatureEntities = new List<ICreature>();
            return base.OnActivateAsync();
        }

        public Task<IReadOnlyCollection<IEntity>> Collision(IEntity entity)
        {
            return CollisionInChunk(entity);
        }

        public Task<IReadOnlyCollection<IPlayer>> CollisionPlayer(IEntity entity)
        {
            return Task.FromResult<IReadOnlyCollection<IPlayer>>(_playerEntities);
        }

        public Task<IReadOnlyCollection<ICollectable>> CollisionCollectable(IEntity entity)
        {
            return Task.FromResult<IReadOnlyCollection<ICollectable>>(_collectableEntities);
        }

        public Task<IReadOnlyCollection<ICreature>> CollisionCreature(IEntity entity)
        {
            return Task.FromResult<IReadOnlyCollection<ICreature>>(_creatureEntities);
        }

        public Task<IReadOnlyCollection<IEntity>> CollisionInChunk(IEntity entity)
        {
            List<IEntity> result = new List<IEntity>();
            foreach (ICollectable each in _collectableEntities)
            {
                result.Add(each);
            }

            foreach (ICreature each in _creatureEntities)
            {
                result.Add(each);
            }

            return Task.FromResult<IReadOnlyCollection<IEntity>>(result);
        }

        public Task Register(IPlayer entity)
        {
            if (!_playerEntities.Contains(entity))
                _playerEntities.Add(entity);

            return Task.CompletedTask;
        }

        public Task Register(ICollectable entity)
        {
            if (!_collectableEntities.Contains(entity))
                _collectableEntities.Add(entity);

            return Task.CompletedTask;
        }

        public Task Register(ICreature entity)
        {
            if (!_creatureEntities.Contains(entity))
                _creatureEntities.Add(entity);

            return Task.CompletedTask;
        }

        public Task Unregister(IPlayer entity)
        {
            _playerEntities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task Unregister(ICollectable entity)
        {
            _collectableEntities.Remove(entity);
            return Task.CompletedTask;
        }

        public Task Unregister(ICreature entity)
        {
            _creatureEntities.Remove(entity);
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
