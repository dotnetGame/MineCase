using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Util.Math
{
    public struct Vector3i
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public Vector3i(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public struct Vector3f
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public Vector3f(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
