using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Graphics
{
    public enum ShapeType : uint
    {
        Cuboid = 0
    }

    [Serializable]
    public abstract class Shape
    {
        public abstract ShapeType Type { get; }

        public abstract bool CollideWith(Shape other);
    }
}
