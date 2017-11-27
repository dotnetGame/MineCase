using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Graphics
{
    [Serializable]
    public class Cuboid : Shape, IEquatable<Cuboid>
    {
        public override ShapeType Type => ShapeType.Cuboid;

        public Point3d Point { get; set; }

        public Size Size { get; set; }

        public Cuboid(Point3d point, Size size)
        {
            Point = point;
            Size = size;
        }

        public bool CollideWithCuboid(Cuboid cuboid)
        {
            if (Point.Y + Size.Height < cuboid.Point.Y ||
                Point.Y > cuboid.Point.Y + cuboid.Size.Height)
                return false;
            Rect rect1 = new Rect
            {
                X = Point.X,
                Z = Point.Z,
                Length = Size.Length,
                Width = Size.Width
            };
            Rect rect2 = new Rect
            {
                X = cuboid.Point.X,
                Z = cuboid.Point.Z,
                Length = cuboid.Size.Length,
                Width = cuboid.Size.Width
            };
            return rect1.OverlapWith(rect2);
        }

        public override bool CollideWith(Shape other)
        {
            switch (other.Type)
            {
                case ShapeType.Cuboid:
                    return CollideWithCuboid((Cuboid)other);
                default:
                    break;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cuboid);
        }

        public bool Equals(Cuboid other)
        {
            return other != null &&
                   EqualityComparer<Point3d>.Default.Equals(Point, other.Point) &&
                   EqualityComparer<Size>.Default.Equals(Size, other.Size);
        }

        public override int GetHashCode()
        {
            var hashCode = 1392910933;
            hashCode = hashCode * -1521134295 + EqualityComparer<Point3d>.Default.GetHashCode(Point);
            hashCode = hashCode * -1521134295 + EqualityComparer<Size>.Default.GetHashCode(Size);
            return hashCode;
        }

        public static bool operator ==(Cuboid cuboid1, Cuboid cuboid2)
        {
            return EqualityComparer<Cuboid>.Default.Equals(cuboid1, cuboid2);
        }

        public static bool operator !=(Cuboid cuboid1, Cuboid cuboid2)
        {
            return !(cuboid1 == cuboid2);
        }
    }
}
