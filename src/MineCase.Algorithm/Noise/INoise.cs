using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    public interface INoise
    {
        float Noise(float x, float y, float z);

        void Noise(float[,,] noise, Vector3 offset, Vector3 scale);
    }
}
