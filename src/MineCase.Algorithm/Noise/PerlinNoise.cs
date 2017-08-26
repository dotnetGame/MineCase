using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm.Noise
{
    /// <summary>
    /// Implementation for Improved Perlin Noise (http://mrl.nyu.edu/~perlin/noise/)
    /// </summary>
    public class PerlinNoise : INoise
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

        public double Noise(double x, double y, double z)
        {
            var xcoord = Split(x);
            var ycoord = Split(y);
            var zcoord = Split(z);

            var u = Fade(xcoord.reminder);
            var v = Fade(ycoord.reminder);
            var w = Fade(zcoord.reminder);

            int aaa, aba, aab, abb, baa, bba, bab, bbb;
            aaa = _p[_p[_p[xcoord.integer] + ycoord.integer] + zcoord.integer];
            aba = _p[_p[_p[xcoord.integer] + ycoord.integer + 1] + zcoord.integer];
            aab = _p[_p[_p[xcoord.integer] + ycoord.integer] + zcoord.integer + 1];
            abb = _p[_p[_p[xcoord.integer] + ycoord.integer + 1] + zcoord.integer + 1];
            baa = _p[_p[_p[xcoord.integer + 1] + ycoord.integer] + zcoord.integer];
            bba = _p[_p[_p[xcoord.integer + 1] + ycoord.integer + 1] + zcoord.integer];
            bab = _p[_p[_p[xcoord.integer + 1] + ycoord.integer] + zcoord.integer + 1];
            bbb = _p[_p[_p[xcoord.integer + 1] + ycoord.integer + 1] + zcoord.integer + 1];

            double x1, x2, y1, y2;
            x1 = Lerp(
                Grad(aaa, xcoord.reminder, ycoord.reminder, zcoord.reminder),
                Grad(baa, xcoord.reminder - 1, ycoord.reminder, zcoord.reminder),
                u);
            x2 = Lerp(
                Grad(aba, xcoord.reminder, ycoord.reminder - 1, zcoord.reminder),
                Grad(bba, xcoord.reminder - 1, ycoord.reminder - 1, zcoord.reminder),
                u);
            y1 = Lerp(x1, x2, v);

            x1 = Lerp(
                Grad(aab, xcoord.reminder, ycoord.reminder, zcoord.reminder - 1),
                Grad(bab, xcoord.reminder - 1, ycoord.reminder, zcoord.reminder - 1),
                u);
            x2 = Lerp(
                Grad(abb, xcoord.reminder, ycoord.reminder - 1, zcoord.reminder - 1),
                Grad(bbb, xcoord.reminder - 1, ycoord.reminder - 1, zcoord.reminder - 1),
                u);
            y2 = Lerp(x1, x2, v);

            return (Lerp(y1, y2, w) + 1) / 2;
        }

        private static (int integer, double reminder) Split(double value)
        {
            var integer = (int)value;
            return (integer % 256, value - integer);
        }

        private static double Fade(double t)
        {
            // 6t^5 - 15t^4 + 10t^3
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        // Source: http://riven8192.blogspot.com/2010/08/calculate-perlinnoise-twice-as-fast.html
        public static double Grad(int hash, double x, double y, double z)
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

        public static double Lerp(double a, double b, double x) =>
            a + x * (b - a);
    }
}
