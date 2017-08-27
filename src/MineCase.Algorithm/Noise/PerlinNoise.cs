using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    /// <summary>
    /// Implementation for Improved Perlin Noise (http://mrl.nyu.edu/~perlin/noise/)
    /// </summary>
    public class PerlinNoise : NoiseBase, INoise
    {
        /// <summary>
        /// Permutation
        /// </summary>
        private readonly int[] _p = new int[512];

        /// <summary>
        /// Initializes a new instance of the <see cref="PerlinNoise"/> class.
        /// </summary>
        /// <param name="seed">Seed for generating permutaion.</param>
        public PerlinNoise(int seed)
        {
            var random = new Random(seed);
            for (int i = 0; i < 256; i++)
                _p[i + 256] = _p[i] = random.Next(0, 256);
        }

        public override float Noise(float x, float y, float z)
        {
            var xcoord = Split(x);
            var ycoord = Split(y);
            var zcoord = Split(z);

            var u = Fade(xcoord.remainder);
            var v = Fade(ycoord.remainder);
            var w = Fade(zcoord.remainder);

            int a = _p[xcoord.integer];
            int b = _p[xcoord.integer + 1];
            int aa = _p[a + ycoord.integer];
            int ab = _p[a + ycoord.integer + 1];
            int ba = _p[b + ycoord.integer];
            int bb = _p[b + ycoord.integer + 1];

            int aaa = _p[aa + zcoord.integer];
            int aba = _p[ab + zcoord.integer];
            int aab = _p[aa + zcoord.integer + 1];
            int abb = _p[ab + zcoord.integer + 1];
            int baa = _p[ba + zcoord.integer];
            int bba = _p[bb + zcoord.integer];
            int bab = _p[ba + zcoord.integer + 1];
            int bbb = _p[bb + zcoord.integer + 1];

            var xa = new Vector4(
                Grad(aaa, xcoord.remainder, ycoord.remainder, zcoord.remainder),
                Grad(aba, xcoord.remainder, ycoord.remainder - 1, zcoord.remainder),
                Grad(aab, xcoord.remainder, ycoord.remainder, zcoord.remainder - 1),
                Grad(abb, xcoord.remainder, ycoord.remainder - 1, zcoord.remainder - 1));
            var xb = new Vector4(
                Grad(baa, xcoord.remainder - 1, ycoord.remainder, zcoord.remainder),
                Grad(bba, xcoord.remainder - 1, ycoord.remainder - 1, zcoord.remainder),
                Grad(bab, xcoord.remainder - 1, ycoord.remainder, zcoord.remainder - 1),
                Grad(bbb, xcoord.remainder - 1, ycoord.remainder - 1, zcoord.remainder - 1));
            var xl = Vector4.Lerp(xa, xb, u);
            var ya = new Vector2(xl.X, xl.Z);
            var yb = new Vector2(xl.Y, xl.W);
            var yl = Vector2.Lerp(ya, yb, v);

            return (Lerp(yl.X, yl.Y, w) + 1) / 2;
        }

        private static (int integer, float remainder) Split(float value)
        {
            var integer = (int)value;
            return (integer % 256, value - integer);
        }

        private static float Fade(float t)
        {
            // 6t^5 - 15t^4 + 10t^3
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        // Source: http://riven8192.blogspot.com/2010/08/calculate-perlinnoise-twice-as-fast.html
        public static float Grad(int hash, float x, float y, float z)
        {
            switch (hash & 0xF)
            {
                case 0x0: return x + y;
                case 0x1: return -x + y;
                case 0x2: return x - y;
                case 0x3: return -x - y;
                case 0x4: return x + z;
                case 0x5: return -x + z;
                case 0x6: return x - z;
                case 0x7: return -x - z;
                case 0x8: return y + z;
                case 0x9: return -y + z;
                case 0xA: return y - z;
                case 0xB: return -y - z;
                case 0xC: return y + x;
                case 0xD: return -y + z;
                case 0xE: return y - x;
                case 0xF: return -y - z;
                default: throw new ArgumentException();
            }
        }

        public static float Lerp(float a, float b, float x) =>
            a + x * (b - a);
    }
}
