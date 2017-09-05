using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Algorithm
{
    public struct UniformRNG
    {
        private UInt64 _state;

        public UniformRNG(UInt64 state)
        {
            _state = state == 0 ? 0xFFFFFFFF : state;
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

        public float Uniform(float a, float b)
        {
           return _state * (b - a) + a;
        }

        public double Uniform(double a, double b)
        {
            return _state * (b - a) + a;
        }

        private uint Next()
        {
            // _state ^= _state >> 13;
            // _state ^= (_state << 7) | 0x9d2c5680;
            // _state ^= (_state << 15) & 0xefc60000;
            // _state = (_state << 32) | (_state >> 17) ^ 0xa3d40a0b;
            _state = (UInt64)(uint)_state * 4164903690 + (uint)(_state >> 32);
            return (uint)_state;
        }
    }
}
