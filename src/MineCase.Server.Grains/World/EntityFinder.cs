using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World
{
    internal class EntityFinder : Grain, IEntityFinder
    {
        private List<IEntity> _entities;

        public override Task OnActivateAsync()
        {
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
                if (eachEntity.GetType() == typeof(ICollectable))
                {
                    result.Add((ICollectable)eachEntity);
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
    }
}
