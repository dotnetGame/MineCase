using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    public struct Rect
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public Rect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rect(Vector2 pos, Vector2 size)
        {
            X = pos.X;
            Y = pos.Y;
            Width = size.X;
            Height = size.Y;
        }

        public Vector2 BottomLeft()
        {
            return new Vector2(X, Y);
        }

        public Vector2 TopRight()
        {
            return new Vector2(X + Width, Y + Height);
        }

        public Vector2 Center()
        {
            return new Vector2(X + Width * 0.5f, Y + Height * 0.5f);
        }

        public bool OverlapWith(Rect rect)
        {
            var minx = Math.Max(X, rect.X);
            var miny = Math.Max(Y, rect.Y);
            var maxx = Math.Min(X + Width, rect.X + rect.Width);
            var maxy = Math.Min(Y + Height, rect.Y + rect.Height);

            // 边重叠也算重叠
            if (minx > maxx || miny > maxy)
                return false;
            return true;
        }

        public bool OverlapWith(Circle circle)
        {
            var c = Center();
            var h = TopRight() - c;
            var v = new Vector2(Math.Abs(circle.Center.X - c.X), Math.Abs(circle.Center.Y - c.Y));
            var u = v - h;
            if (u.X < 0 && u.Y < 0)
            {
                u.X = u.Y = 0;
            }

            return u.LengthSquared() <= circle.Radius * circle.Radius;
        }
    }
}
