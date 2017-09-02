using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World
{
    internal class CollectableFinder : Grain, ICollectableFinder
    {
        private List<ICollectable> _collectables;

        public override Task OnActivateAsync()
        {
            _collectables = new List<ICollectable>();
            return base.OnActivateAsync();
        }

        public Task<IReadOnlyCollection<ICollectable>> Collision(IEntity entity)
        {
            return CollisionInChunk(entity);
        }

        public Task<IReadOnlyCollection<ICollectable>> CollisionInChunk(IEntity entity)
        {
            return Task.FromResult<IReadOnlyCollection<ICollectable>>(_collectables);
        }

        public Task Register(ICollectable collectable)
        {
            if (!_collectables.Contains(collectable))
                _collectables.Add(collectable);
            return Task.CompletedTask;
        }

        public Task Unregister(ICollectable collectable)
        {
            _collectables.Remove(collectable);
            return Task.CompletedTask;
        }
    }
}
