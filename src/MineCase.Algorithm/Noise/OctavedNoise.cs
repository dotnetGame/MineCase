using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    public class OctavedNoise<TNoise> : NoiseBase, INoise
        where TNoise : INoise
    {
        private readonly TNoise _innerNoise;
        private readonly int _octaves;
        private readonly double _persistence;

        public OctavedNoise(TNoise innerNoise, int octaves, double persistence)
        {
            _innerNoise = innerNoise;
            _octaves = octaves;
            _persistence = persistence;
        }

        public override double Noise(double x, double y, double z)
        {
            double total = 0;
            double frequency = 1;
            double amplitude = 1;
            double maxValue = 0;

            for (int i = 0; i < _octaves; i++)
            {
                total += _innerNoise.Noise(x * frequency, y * frequency, z * frequency) * amplitude;
                maxValue += amplitude;
                amplitude *= _persistence;
                frequency *= 2;
            }

            return total / maxValue;
        }
    }
}
