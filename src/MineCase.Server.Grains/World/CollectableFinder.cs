﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    [Reentrant]
    internal class CollectableFinder : Grain, ICollectableFinder
    {
        private IWorld _world;

        /*
        private List<ICollectable> _collectables;
        */
        public override Task OnActivateAsync()
        {
            _world = GrainFactory.GetGrain<IWorld>(this.GetWorldAndChunkWorldPos().worldKey);

            // _collectables = new List<ICollectable>();
            return base.OnActivateAsync();
        }

        /*
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
        }*/

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
    }
}
