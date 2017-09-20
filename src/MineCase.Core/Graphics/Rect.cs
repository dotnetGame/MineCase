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

        public float Length { get; set; }

        public float Width { get; set; }

        public Rect(float x, float y, float length, float width)
        {
            X = x;
            Y = y;
            Length = length;
            Width = width;
        }

        public Rect(Point2d point, Size size)
        {
            X = point.X;
            Y = point.Y;
            Length = size.Length;
            Width = size.Width;
        }

        public Point2d BottomLeft()
        {
            return new Point2d(X, Y);
        }

        public Point2d TopRight()
        {
            return new Point2d(X + Length, Y + Width);
        }

        public Point2d Center()
        {
            return new Point2d(X + Length * 0.5f, Y + Width * 0.5f);
        }

        public bool OverlapWith(Rect rect)
        {
            var minx = Math.Max(X, rect.X);
            var miny = Math.Max(Y, rect.Y);
            var maxx = Math.Min(X + Length, rect.X + rect.Length);
            var maxy = Math.Min(Y + Width, rect.Y + rect.Width);

            // 边重叠也算重叠
            if (minx > maxx || miny > maxy)
                return false;
            return true;
        }

        public bool OverlapWith(Circle circle)
        {
            var c = Center();
            var h = TopRight() - c;
            var v = new Point2d(Math.Abs(circle.Center.X - c.X), Math.Abs(circle.Center.Y - c.Y));
            var u = v - h;
            if (u.X < 0f && u.Y < 0f)
            {
                u.X = u.Y = 0f;
            }

            return u.LengthSquared() <= circle.Radius * circle.Radius;
        }
    }
}
