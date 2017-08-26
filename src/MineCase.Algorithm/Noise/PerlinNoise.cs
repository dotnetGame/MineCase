using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    public class PerlinNoise
    {
        private readonly int[] _permutation = new int[512];

        public PerlinNoise(int seed)
        {
            var random = new Random(seed);
            for (int i = 0; i < 256; i++)
                _permutation[i + 256] = _permutation[i] = random.Next(0, 256);
        }

        public double Noise(double x, double y, double z)
        {
            return 255;
        }
    }
}
