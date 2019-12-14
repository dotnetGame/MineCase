using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.World.Biome
{
    public class BiomeProperties
    {
        public string BiomeName { get; set; } = "InvalidBiome";

        public BiomeId BiomeId { get; set; } = BiomeId.Ocean;

        /** The base height of this biome. Default 0.1. */
        public float BaseHeight { get; set; } = 0.1F;

        /** The variation from the base height of the biome. Default 0.2. */
        public float HeightVariation { get; set; } = 0.2F;

        /** The temperature of this biome. */
        public float Temperature { get; set; } = 0.5F;

        /** The rainfall in this biome. */
        public float Rainfall { get; set; } = 0.5F;

        /** Color tint applied to water depending on biome */
        public int WaterColor { get; set; } = 16777215;

        /** Set to true if snow is enabled for this biome. */
        public bool EnableSnow { get; set; } = false;

        /** Is true (default) if the biome support rain (desert and nether can't have rain) */
        public bool EnableRain { get; set; } = true;
    }
}
