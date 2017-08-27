using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    public interface INoise
    {
        double Noise(double x, double y, double z);

        void Noise(double[,,] noise, Vector3 offset, Vector3 scale);
    }
}
