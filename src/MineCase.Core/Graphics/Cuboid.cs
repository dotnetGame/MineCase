using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Graphics
{
    [Serializable]
    public class Cuboid : Shape
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
    }
}
