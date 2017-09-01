using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game
{
    public interface IEntity : IGrainWithStringKey
    {
        Task<Vector3> GetPosition();

        Task SetPosition(Vector3 position);

        Task<(int x, int y, int z)> GetChunkPosition();
    }

    public static class EntityExtensions
    {
        public static string MakeEntityKey(this IWorld world, uint eid)
        {
            return $"{world.GetPrimaryKeyString()},{eid}";
        }

        public static uint GetEntityId(this IEntity entity)
        {
            var key = entity.GetPrimaryKeyString();
            return uint.Parse(key.Split(',')[1]);
        }

        public static (string worldKey, uint entityId) GetWorldAndEntityId(this IEntity entity)
        {
            var key = entity.GetPrimaryKeyString().Split(',');
            return (key[0], uint.Parse(key[1]));
        }
    }
}
