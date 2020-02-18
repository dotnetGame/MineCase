using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World.Biome
{
    public class BiomeConstants
    {
        public static readonly int XZBiomeNum = (int)Math.Round(Math.Log(16.0D) / Math.Log(2.0D)) - 2;
        public static readonly int YBiomeNum = (int)Math.Round(Math.Log(256.0D) / Math.Log(2.0D)) - 2;
        public static readonly int ChunkBiomeNum = 1 << XZBiomeNum + XZBiomeNum + YBiomeNum;
        public static readonly int XZMask = (1 << XZBiomeNum) - 1;
        public static readonly int YMask = (1 << YBiomeNum) - 1;
    }
}
