using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    public struct Circle
    {
        public Vector2 Center { get; set; }

        public float Radius { get; set; }

        public Circle(float x, float y, float radius)
        {
            Center = new Vector2(x, y);
            Radius = radius;
        }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool OverlapWith(Rect rect)
        {
            return rect.OverlapWith(this);
        }
    }
}
