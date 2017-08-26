using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    public interface INoise
    {
        double Noise(double x, double y, double z);
    }
}
