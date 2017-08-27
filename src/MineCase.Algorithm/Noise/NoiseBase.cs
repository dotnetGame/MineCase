using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    public abstract class NoiseBase : INoise
    {
        public abstract double Noise(double x, double y, double z);

        public virtual void Noise(double[,,] noise, Vector3 offset, Vector3 scale)
        {
            var xExtent = noise.GetUpperBound(0) + 1;
            var yExtent = noise.GetUpperBound(1) + 1;
            var zExtent = noise.GetUpperBound(2) + 1;

            for (int z = 0; z < zExtent; z++)
            {
                var zOffset = offset.Z + z * scale.Z;
                for (int y = 0; y < yExtent; y++)
                {
                    var yOffset = offset.Y + y * scale.Y;
                    for (int x = 0; x < xExtent; x++)
                    {
                        var xOffset = offset.X + x * scale.X;
                        noise[x, y, z] = Noise(xOffset, yOffset, zOffset);
                    }
                }
            }
        }
    }
}
