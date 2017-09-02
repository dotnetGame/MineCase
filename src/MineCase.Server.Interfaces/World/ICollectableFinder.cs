using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.Server.Game.Entities;
using Orleans;

namespace MineCase.Server.World
{
    public interface ICollectableFinder : IGrainWithStringKey
    {
        Task Register(ICollectable collectable);

        Task Unregister(ICollectable collectable);

        Task<IReadOnlyCollection<ICollectable>> Collision(IEntity entity);

        Task<IReadOnlyCollection<ICollectable>> CollisionInChunk(IEntity entity);
    }

    public static class CollectableFinderExtensions
    {
        public static string MakeCollectableFinderKey(this IWorld world, int x, int z)
        {
            return $"{world.GetPrimaryKeyString()},{x},{z}";
        }

        public static (int x, int z) GetChunkPosition(this ICollectableFinder collectableFinder)
        {
            var key = collectableFinder.GetPrimaryKeyString().Split(',');
            return (int.Parse(key[1]), int.Parse(key[2]));
        }

        public static (string worldKey, int x, int z) GetWorldAndChunkPosition(this ICollectableFinder collectableFinder)
        {
            var key = collectableFinder.GetPrimaryKeyString().Split(',');
            return (key[0], int.Parse(key[1]), int.Parse(key[2]));
        }
    }
}
