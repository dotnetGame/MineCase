using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    public struct Point2d
    {
        public float X { get; set; }

        public float Y { get; set; }

        public Point2d(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Point2d(Point2d point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Point2d(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public static float Distance(Point2d p1, Point2d p2)
        {
            return (float)Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X)) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public float LengthSquared()
        {
            return Length() * Length();
        }

        public static Point2d operator + (Point2d p1, Point2d p2)
        {
            return new Point2d(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point2d operator - (Point2d value)
        {
            return new Point2d(-value.X, -value.Y);
        }

        public static Point2d operator - (Point2d p1, Point2d p2)
        {
            return new Point2d(p1.X - p2.X, p1.Y - p2.Y);
        }
    }

    public struct Point3d
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public Point3d(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3d(Point3d point)
        {
            X = point.X;
            Y = point.Y;
            Z = point.Z;
        }

        public Point3d(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }
    }
}
