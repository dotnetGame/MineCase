using System;
using System.Numerics;

namespace MineCase.Noise
{
    public class OctavedNoise<TNoise>
        where TNoise : INoise
    {
        private readonly TNoise _innerNoise;
        private readonly int _octaves;
        private readonly float _persistence;

        public OctavedNoise(TNoise innerNoise, int octaves, float persistence)
        {
            _innerNoise = innerNoise;
            _octaves = octaves;
            _persistence = persistence;
        }

        public float Noise(float x, float y, float z)
        {
            double total = 0;
            int frequency = 1;
            double amplitude = 1;
            double maxValue = 0;

            for (int i = 0; i < _octaves; i++)
            {
                total += _innerNoise.Noise(x * frequency, y * frequency, z * frequency) * amplitude;
                maxValue += amplitude;
                amplitude *= _persistence;
                frequency *= 2;
            }

            return (float)(total / maxValue);
        }

        public void Noise(float[,,] noise, Vector3 offset, Vector3 scale)
        {
            Array.Clear(noise, 0, noise.Length);
            int frequency = 1;
            double amplitude = 1;
            double maxValue = 0;

            for (int i = 0; i < _octaves; i++)
            {
                _innerNoise.AddNoise(noise, Vector3.Multiply(offset, scale) * frequency, scale * frequency, (float)amplitude);
                maxValue += amplitude;
                amplitude *= _persistence;
                frequency *= 2;
            }

            var xExtent = noise.GetUpperBound(0) + 1;
            var yExtent = noise.GetUpperBound(1) + 1;
            var zExtent = noise.GetUpperBound(2) + 1;
            for (int x = 0; x < xExtent; x++)
            {
                for (int y = 0; y < yExtent; y++)
                {
                    for (int z = 0; z < zExtent; z++)
                        noise[x, y, z] /= (float)maxValue;
                }
            }
        }
    }
}
