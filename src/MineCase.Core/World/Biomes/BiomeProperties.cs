using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World.Biomes
{
    public class BiomeProperties
    {
        public string BiomeName { get; set; }

        public BiomeId BiomeId { get; set; }

        public float BaseHeight { get; set; } = 0.1F;

        public float HeightVariation { get; set; } = 0.2F;

        public float Temperature { get; set; } = 0.5F;

        public float Rainfall { get; set; } = 0.5F;

        public int WaterColor { get; set; } = 16777215;

        public bool EnableSnow { get; set; } = false;

        public bool EnableRain { get; set; } = true;
    }
}
