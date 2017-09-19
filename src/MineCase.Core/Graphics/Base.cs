using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    public static class Base
    {
        public static float Distance(Vector2 p1, Vector2 p2)
        {
            return (float)Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X)) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
    }
}
