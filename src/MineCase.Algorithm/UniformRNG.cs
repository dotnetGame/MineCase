﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm
{
    public struct UniformRNG
    {
        // https://en.wikipedia.org/wiki/Linear_congruential_generator
        // MMIX by Donald Knuth
        private static readonly ulong _multiplier = 6364136223846793005;
        private static readonly ulong _increment = 1442695040888963407;

        private ulong _state;

        public UniformRNG(ulong state)
        {
            if (state == 0)
            {
                var time = DateTime.Now;
                var seed = (ulong)(time.Day << 25 | time.Hour << 20 | time.Minute << 14 | time.Second << 8 | time.Millisecond);
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

        public int NextInt32()
        {
            return (int)Next();
        }

        public uint NextUInt32()
        {
            return Next();
        }

        public long NextInt64()
        {
            Next();
            return (long)_state;
        }

        public ulong NextUInt64()
        {
            Next();
            return _state;
        }

        public float NextSingle()
        {
            return Next() * 2.3283064365386962890625e-10f;
        }

        public double NextDouble()
        {
            var t = Next();
            return (((ulong)t << 32) | Next()) * 5.4210108624275221700372640043497e-20;
        }

        public int Uniform(int a, int b)
        {
            return a == b ? a : (int)(Next() % (b - a) + a);
        }

        public uint Uniform(uint a, uint b)
        {
            return a == b ? a : Next() % (b - a) + a;
        }

        public long Uniform(long a, long b)
        {
            return a == b ? a : Next() % (b - a) + a;
        }

        public ulong Uniform(ulong a, ulong b)
        {
            return a == b ? a : Next() % (b - a) + a;
        }

        public float Uniform(float a, float b)
        {
            return NextSingle() * (b - a) + a;
        }

        public double Uniform(double a, double b)
        {
            return NextDouble() * (b - a) + a;
        }

        private uint Next()
        {
            _state = _state * _multiplier + _increment;
            return (uint)_state;
        }
    }
}
