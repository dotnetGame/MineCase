using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Graphics;
using MineCase.Server.Game;
using MineCase.World;

namespace MineCase.Algorithm
{
    public static class Collsion
    {
        public static async Task<List<ValueTuple>> EntityBlockCollide(IEntity entity, ChunkColumnCompactStorage chunkColumnCompactStorage)
        {
        }

        public static async Task<List<IEntity>> EntityEntityCollide(IEntity entity, List<IEntity> entityList)
        {
        }

        private static bool IsCollided(Vector3 pos1, Vector3 bx1, Vector3 pos2, Vector3 bx2)
        {
            if (pos1.Y > pos2.Y + bx2.Y || pos1.Y + bx1.Y < pos2.Y)
                return false;
            Rect rect1 = new Rect
            {
                X = pos1.X,
                Y = pos1.Z,
                Width = bx1.X,
                Height = bx1.Z
            };
            Rect rect2 = new Rect
            {
                X = pos2.X,
                Y = pos2.Z,
                Width = bx2.X,
                Height = bx2.Z
            };
            return rect1.OverlapWith(rect2);
        }
    }
}
