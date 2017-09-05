using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm
{
    public struct UniformRNG
    {
        // https://en.wikipedia.org/wiki/Linear_congruential_generator
        // MMIX by Donald Knuth
        private static readonly UInt64 _multiplier = 6364136223846793005;
        private static readonly UInt64 _increment = 1442695040888963407;
        private static readonly double _divisor = 4294967291.0;

        private UInt64 _state;

        public UniformRNG(UInt64 state)
        {
            if (state == 0)
            {
                DateTime time = DateTime.Now;
                var seed = (UInt64)(time.Day << 25 | time.Hour << 20 | time.Minute << 14 | time.Second << 8 | time.Millisecond);
                for (int i = 0; i < 4; ++i)
                {
                    seed = seed * _multiplier + _increment;
                }

                _state = seed;
            }
            else
            {
                _state = state;
            }
        }

        public int NextInt()
        {
            return (int)Next();
        }

        public uint NextUInt()
        {
            return Next();
        }

        public float NextFloat()
        {
            return Next() * 2.3283064365386962890625e-10f;
        }

        public double NextDouble()
        {
            var t = Next();
            return (((UInt64)t << 32) | Next()) * 5.4210108624275221700372640043497e-20;
        }

        public int Uniform(int a, int b)
        {
            return a == b ? a : (int)(Next() % (b - a) + a);
        }

        public uint Uniform(uint a, uint b)
        {
            return a == b ? a : Next() % (b - a) + a;
        }

        public Int64 Uniform(Int64 a, Int64 b)
        {
            return a == b ? a : Next() % (b - a) + a;
        }

        public UInt64 Uniform(UInt64 a, UInt64 b)
        {
            return a == b ? a : Next() % (b - a) + a;
        }

        public float Uniform(float a, float b)
        {
            return Uniform(0, 4294967292) / (float)_divisor * (b - a) + a;
        }

        public double Uniform(double a, double b)
        {
            return Uniform(0, 4294967292) / _divisor * (b - a) + a;
        }

        private uint Next()
        {
            _state = _state * _multiplier + _increment;
            return (uint)_state;
        }
    }
}
