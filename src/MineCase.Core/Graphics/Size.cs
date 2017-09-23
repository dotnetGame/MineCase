using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text;

namespace MineCase.Graphics
{
    public struct Size
    {
        /// <summary>
        /// Gets or sets the size of X-axis.
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// Gets or sets the size of Z-axis.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        /// Gets or sets the size of Y-axis.
        /// </summary>
        public float Height { get; set; }

        public Size(float length, float width, float height = 0f)
        {
            Contract.Assert(length >= 0f, "The parameter must be greater than or equal to zero.");
            Contract.Assert(width >= 0f, "The parameter must be greater than or equal to zero.");
            Contract.Assert(height >= 0f, "The parameter must be greater than or equal to zero.");
            Length = length;
            Width = width;
            Height = height;
        }

        public Size(Size size)
        {
            Length = size.Length;
            Width = size.Width;
            Height = size.Height;
        }

        public float Area()
        {
            return Length * Width;
        }

        public float Volume()
        {
            return Length * Width * Height;
        }
    }
}
