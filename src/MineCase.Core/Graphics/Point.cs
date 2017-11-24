using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    [Serializable]
    public struct Point2d : IEquatable<Point2d>
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

        public override bool Equals(object obj)
        {
            return obj is Point2d && Equals((Point2d)obj);
        }

        public bool Equals(Point2d other)
        {
            return X == other.X &&
                   Z == other.Z;
        }

        public override int GetHashCode()
        {
            var hashCode = 1911744652;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
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

        public static bool operator ==(Point2d d1, Point2d d2)
        {
            return d1.Equals(d2);
        }

        public static bool operator !=(Point2d d1, Point2d d2)
        {
            return !(d1 == d2);
        }
    }

    [Serializable]
    public struct Point3d : IEquatable<Point3d>
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

        public override bool Equals(object obj)
        {
            return obj is Point3d && Equals((Point3d)obj);
        }

        public bool Equals(Point3d other)
        {
            return X == other.X &&
                   Z == other.Z &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 900060928;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Point3d d1, Point3d d2)
        {
            return d1.Equals(d2);
        }

        public static bool operator !=(Point3d d1, Point3d d2)
        {
            return !(d1 == d2);
        }
    }
}
