using System;
using System.Numerics;

namespace MineCase.Algorithm.Noise
{
    /// <summary>
    /// Implementation for Improved Perlin Noise (http://mrl.nyu.edu/~perlin/noise/).
    /// </summary>
    public class PerlinNoise : INoise
    {
        /// <summary>
        /// Permutation.
        /// </summary>
        private readonly byte[] _p = new byte[512];

        /// <summary>
        /// Initializes a new instance of the <see cref="PerlinNoise"/> class.
        /// </summary>
        /// <param name="seed">Seed for generating permutaion.</param>
        public PerlinNoise(int seed)
        {
            var random = new UniformRNG((ulong)seed);
            for (int i = 0; i < 256; i++)
                _p[i + 256] = _p[i] = (byte)(random.NextUInt32() % 256);
        }

        public float Noise(float x, float y, float z)
        {
            var xcoord = Split(x);
            var ycoord = Split(y);
            var zcoord = Split(z);

            var u = Fade(xcoord.Remainder);
            var v = Fade(ycoord.Remainder);
            var w = Fade(zcoord.Remainder);

            int a = _p[xcoord.Integer];
            int b = _p[xcoord.Integer + 1];
            int aa = _p[a + ycoord.Integer];
            int ab = _p[a + ycoord.Integer + 1];
            int ba = _p[b + ycoord.Integer];
            int bb = _p[b + ycoord.Integer + 1];

            int aaa = _p[aa + zcoord.Integer];
            int aba = _p[ab + zcoord.Integer];
            int aab = _p[aa + zcoord.Integer + 1];
            int abb = _p[ab + zcoord.Integer + 1];
            int baa = _p[ba + zcoord.Integer];
            int bba = _p[bb + zcoord.Integer];
            int bab = _p[ba + zcoord.Integer + 1];
            int bbb = _p[bb + zcoord.Integer + 1];

            var xa = new Vector4(
                Grad(aaa, xcoord.Remainder, ycoord.Remainder, zcoord.Remainder),
                Grad(aba, xcoord.Remainder, ycoord.Remainder - 1, zcoord.Remainder),
                Grad(aab, xcoord.Remainder, ycoord.Remainder, zcoord.Remainder - 1),
                Grad(abb, xcoord.Remainder, ycoord.Remainder - 1, zcoord.Remainder - 1));
            var xb = new Vector4(
                Grad(baa, xcoord.Remainder - 1, ycoord.Remainder, zcoord.Remainder),
                Grad(bba, xcoord.Remainder - 1, ycoord.Remainder - 1, zcoord.Remainder),
                Grad(bab, xcoord.Remainder - 1, ycoord.Remainder, zcoord.Remainder - 1),
                Grad(bbb, xcoord.Remainder - 1, ycoord.Remainder - 1, zcoord.Remainder - 1));
            var xl = Vector4.Lerp(xa, xb, u);
            var ya = new Vector2(xl.X, xl.Z);
            var yb = new Vector2(xl.Y, xl.W);
            var yl = Vector2.Lerp(ya, yb, v);

            return (Lerp(yl.X, yl.Y, w) + 1) / 2;
        }

        public void Noise(float[,,] noise, Vector3 offset, Vector3 scale)
        {
            var xExtent = noise.GetUpperBound(0) + 1;
            var yExtent = noise.GetUpperBound(1) + 1;
            var zExtent = noise.GetUpperBound(2) + 1;

            for (int x = 0; x < xExtent; x++)
            {
                var xOffset = offset.X + x * scale.X;
                var xcoord = Split(xOffset);
                var u = Fade(xcoord.Remainder);

                int a = _p[xcoord.Integer];
                int b = _p[xcoord.Integer + 1];
                for (int y = 0; y < yExtent; y++)
                {
                    var yOffset = offset.Y + y * scale.Y;
                    var ycoord = Split(yOffset);
                    var v = Fade(ycoord.Remainder);

                    int aa = _p[a + ycoord.Integer];
                    int ab = _p[a + ycoord.Integer + 1];
                    int ba = _p[b + ycoord.Integer];
                    int bb = _p[b + ycoord.Integer + 1];
                    for (int z = 0; z < zExtent; z++)
                    {
                        var zOffset = offset.Z + z * scale.Z;
                        var zcoord = Split(zOffset);
                        var w = Fade(zcoord.Remainder);

                        int aaa = _p[aa + zcoord.Integer];
                        int aba = _p[ab + zcoord.Integer];
                        int aab = _p[aa + zcoord.Integer + 1];
                        int abb = _p[ab + zcoord.Integer + 1];
                        int baa = _p[ba + zcoord.Integer];
                        int bba = _p[bb + zcoord.Integer];
                        int bab = _p[ba + zcoord.Integer + 1];
                        int bbb = _p[bb + zcoord.Integer + 1];

                        var xa = new Vector4(
                            Grad(aaa, xcoord.Remainder, ycoord.Remainder, zcoord.Remainder),
                            Grad(aba, xcoord.Remainder, ycoord.Remainder - 1, zcoord.Remainder),
                            Grad(aab, xcoord.Remainder, ycoord.Remainder, zcoord.Remainder - 1),
                            Grad(abb, xcoord.Remainder, ycoord.Remainder - 1, zcoord.Remainder - 1));
                        var xb = new Vector4(
                            Grad(baa, xcoord.Remainder - 1, ycoord.Remainder, zcoord.Remainder),
                            Grad(bba, xcoord.Remainder - 1, ycoord.Remainder - 1, zcoord.Remainder),
                            Grad(bab, xcoord.Remainder - 1, ycoord.Remainder, zcoord.Remainder - 1),
                            Grad(bbb, xcoord.Remainder - 1, ycoord.Remainder - 1, zcoord.Remainder - 1));
                        var xl = Vector4.Lerp(xa, xb, u);
                        var ya = new Vector2(xl.X, xl.Z);
                        var yb = new Vector2(xl.Y, xl.W);
                        var yl = Vector2.Lerp(ya, yb, v);

                        noise[x, y, z] = (Lerp(yl.X, yl.Y, w) + 1) / 2;
                    }
                }
            }
        }

        public void AddNoise(float[,,] noise, Vector3 offset, Vector3 scale, float noiseScale)
        {
            var xExtent = noise.GetUpperBound(0) + 1;
            var yExtent = noise.GetUpperBound(1) + 1;
            var zExtent = noise.GetUpperBound(2) + 1;

            for (int x = 0; x < xExtent; x++)
            {
                var xOffset = offset.X + x * scale.X;
                var xcoord = Split(xOffset);
                var u = Fade(xcoord.Remainder);

                int a = _p[xcoord.Integer];
                int b = _p[xcoord.Integer + 1];
                for (int y = 0; y < yExtent; y++)
                {
                    var yOffset = offset.Y + y * scale.Y;
                    var ycoord = Split(yOffset);
                    var v = Fade(ycoord.Remainder);

                    int aa = _p[a + ycoord.Integer];
                    int ab = _p[a + ycoord.Integer + 1];
                    int ba = _p[b + ycoord.Integer];
                    int bb = _p[b + ycoord.Integer + 1];
                    for (int z = 0; z < zExtent; z++)
                    {
                        var zOffset = offset.Z + z * scale.Z;
                        var zcoord = Split(zOffset);
                        var w = Fade(zcoord.Remainder);

                        int aaa = _p[aa + zcoord.Integer];
                        int aba = _p[ab + zcoord.Integer];
                        int aab = _p[aa + zcoord.Integer + 1];
                        int abb = _p[ab + zcoord.Integer + 1];
                        int baa = _p[ba + zcoord.Integer];
                        int bba = _p[bb + zcoord.Integer];
                        int bab = _p[ba + zcoord.Integer + 1];
                        int bbb = _p[bb + zcoord.Integer + 1];

                        var xa = new Vector4(
                            Grad(aaa, xcoord.Remainder, ycoord.Remainder, zcoord.Remainder),
                            Grad(aba, xcoord.Remainder, ycoord.Remainder - 1, zcoord.Remainder),
                            Grad(aab, xcoord.Remainder, ycoord.Remainder, zcoord.Remainder - 1),
                            Grad(abb, xcoord.Remainder, ycoord.Remainder - 1, zcoord.Remainder - 1));
                        var xb = new Vector4(
                            Grad(baa, xcoord.Remainder - 1, ycoord.Remainder, zcoord.Remainder),
                            Grad(bba, xcoord.Remainder - 1, ycoord.Remainder - 1, zcoord.Remainder),
                            Grad(bab, xcoord.Remainder - 1, ycoord.Remainder, zcoord.Remainder - 1),
                            Grad(bbb, xcoord.Remainder - 1, ycoord.Remainder - 1, zcoord.Remainder - 1));
                        var xl = Vector4.Lerp(xa, xb, u);
                        var ya = new Vector2(xl.X, xl.Z);
                        var yb = new Vector2(xl.Y, xl.W);
                        var yl = Vector2.Lerp(ya, yb, v);

                        noise[x, y, z] += (Lerp(yl.X, yl.Y, w) + 1) / 2 * noiseScale;
                    }
                }
            }
        }

        private static (int Integer, float Remainder) Split(float value)
        {
            value = value % 256;
            if (value < 0) value += 256;
            var integer = (int)value;
            var remainder = value - integer;
            return (integer, remainder);
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