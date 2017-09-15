using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    public interface IEntityFinder : IGrainWithStringKey
    {
        Task Register(ICollectable entity);

        Task Register(ICreature entity);

        Task Unregister(ICollectable entity);

        Task Unregister(ICreature entity);

        Task<IReadOnlyCollection<IEntity>> Collision(IEntity entity);

        Task<IReadOnlyCollection<ICollectable>> CollisionCollectable(IEntity entity);

        Task<IReadOnlyCollection<ICreature>> CollisionCreature(IEntity entity);

        Task<IReadOnlyCollection<IEntity>> CollisionInChunk(IEntity entity);

        Task SpawnPickup(Position location, Immutable<Slot[]> slots);
    }

    public static class EntityFinderExtensions
    {
        public static string MakeEntityFinderKey(this IWorld world, int x, int z)
        {
            return $"{world.GetPrimaryKeyString()},{x},{z}";
        }

        public static (int x, int z) GetChunkPosition(this IEntityFinder entityFinder)
        {
            var key = entityFinder.GetPrimaryKeyString().Split(',');
            return (int.Parse(key[1]), int.Parse(key[2]));
        }

        public static (string worldKey, int x, int z) GetWorldAndChunkPosition(this IEntityFinder collectableFinder)
        {
            var key = collectableFinder.GetPrimaryKeyString().Split(',');
            return (key[0], int.Parse(key[1]), int.Parse(key[2]));
        }
    }
}
