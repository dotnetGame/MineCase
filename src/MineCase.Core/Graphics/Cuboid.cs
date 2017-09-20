using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Graphics
{
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
            if (Point.Z + Size.Height < cuboid.Point.Z ||
                Point.Z > cuboid.Point.Z + cuboid.Size.Height)
                return false;
            Rect rect1 = new Rect
            {
                X = Point.X,
                Y = Point.Y,
                Length = Size.Length,
                Width = Size.Width
            };
            Rect rect2 = new Rect
            {
                X = cuboid.Point.X,
                Y = cuboid.Point.Y,
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
