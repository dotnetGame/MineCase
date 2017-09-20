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
    public static class Collision
    {
        public static bool IsCollided(Shape shape1, Shape shape2)
        {
            return shape1.CollideWith(shape2);
        }
    }
}
