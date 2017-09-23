using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    public struct Point2d
    {
        /// <summary>
        /// Gets or sets the positon of X-axis in minecraft world.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Gets or sets the positon of Z-axis in minecraft world.
        /// </summary>
        public float Z { get; set; }

        public Point2d(float x, float z)
        {
            X = x;
            Z = z;
        }

        public Point2d(Point2d point)
        {
            X = point.X;
            Z = point.Z;
        }

        public Point2d(Vector2 vector)
        {
            X = vector.X;
            Z = vector.Y;
        }

        public static float Distance(Point2d p1, Point2d p2)
        {
            return (float)Math.Sqrt(((p1.X - p2.X) * (p1.X - p2.X)) + (p1.Z - p2.Z) * (p1.Z - p2.Z));
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Z * Z);
        }

        public float LengthSquared()
        {
            return X * X + Z * Z;
        }

        public static Point2d operator + (Point2d p1, Point2d p2)
        {
            return new Point2d(p1.X + p2.X, p1.Z + p2.Z);
        }

        public static Point2d operator - (Point2d value)
        {
            return new Point2d(-value.X, -value.Z);
        }

        public static Point2d operator - (Point2d p1, Point2d p2)
        {
            return new Point2d(p1.X - p2.X, p1.Z - p2.Z);
        }
    }

    public struct Point3d
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
        /// Gets or sets the positon of Y-axis in minecraft world.
        /// </summary>
        public float Y { get; set; }

        public Point3d(float x, float z, float y)
        {
            X = x;
            Z = z;
            Y = y;
        }

        public Point3d(Point3d point)
        {
            X = point.X;
            Z = point.Z;
            Y = point.Y;
        }

        public Point3d(Vector3 vector)
        {
            X = vector.X;
            Z = vector.Z;
            Y = vector.Y;
        }
    }
}
