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

        public float Width { get; set; }

        public float Height { get; set; }

        public Rect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rect(Vector2 pos, Vector2 size)
        {
            X = pos.X;
            Y = pos.Y;
            Width = size.X;
            Height = size.Y;
        }

        public Vector2 BottomLeft()
        {
            return new Vector2(X, Y);
        }

        public Vector2 TopRight()
        {
            return new Vector2(X + Width, Y + Height);
        }

        public Vector2 Center()
        {
            return new Vector2(X + Width * 0.5f, Y + Height * 0.5f);
        }

        public bool IntersectWith(Rect other)
        {
            return false;
        }
    }
}
