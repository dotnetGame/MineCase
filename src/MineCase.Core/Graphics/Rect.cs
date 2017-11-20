using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    [Serializable]
    public struct Rect
    {
        /// <summary>
        /// Gets or sets the positon of X-axis in minecraft world.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the positon of Z-axis in minecraft world.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Gets or sets the size of X-axis.
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Gets or sets the size of Y-axis.
        /// </summary>
        public float Width { get; set; }

        public Rect(float x, float z, float length, float width)
        {
            X = x;
            Z = z;
            Length = length;
            Width = width;
        }

        public Rect(Point2d point, Size size)
        {
            X = point.X;
            Z = point.Z;
            Length = size.Length;
            Width = size.Width;
        }

        public Point2d BottomLeft()
        {
            return new Point2d(X, Z);
        }

        public Point2d TopRight()
        {
            return new Point2d(X + Length, Z + Width);
        }

        public Point2d Center()
        {
            return new Point2d(X + Length * 0.5f, Z + Width * 0.5f);
        }

        public bool OverlapWith(Rect rect)
        {
            var minx = Math.Max(X, rect.X);
            var minz = Math.Max(Z, rect.Z);
            var maxx = Math.Min(X + Length, rect.X + rect.Length);
            var maxz = Math.Min(Z + Width, rect.Z + rect.Width);

            // 边重叠也算重叠
            if (minx > maxx || minz > maxz)
                return false;
            return true;
        }

        public bool OverlapWith(Circle circle)
        {
            var c = Center();
            var h = TopRight() - c;
            var v = new Point2d(Math.Abs(circle.Center.X - c.X), Math.Abs(circle.Center.Z - c.Z));
            var u = v - h;
            if (u.X < 0f && u.Z < 0f)
            {
                u.X = u.Z = 0f;
            }

            return u.LengthSquared() <= circle.Radius * circle.Radius;
        }
    }
}
