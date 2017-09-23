using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    public struct Circle
    {
        public Point2d Center { get; set; }

        public float Radius { get; set; }

        public Circle(float x, float z, float radius)
        {
            Center = new Point2d(x, z);
            Radius = radius;
        }

        public Circle(Point2d center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool OverlapWith(Circle circle)
        {
            return Radius + circle.Radius < Point2d.Distance(Center, circle.Center);
        }

        public bool OverlapWith(Rect rect)
        {
            return rect.OverlapWith(this);
        }
    }
}
