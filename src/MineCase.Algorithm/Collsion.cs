using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game;
using MineCase.World;

namespace MineCase.Algorithm
{
    public static class Collsion
    {
        public static async Task<List<ValueTuple>> EntityBlockCollide(IEntity entity, ChunkColumnCompactStorage chunkColumnCompactStorage)
        {
            var position = await entity.GetPosition();
        }

        public static async Task<List<IEntity>> EntityEntityCollide(IEntity entity, List<IEntity> entityList)
        {

        }
    }
}
