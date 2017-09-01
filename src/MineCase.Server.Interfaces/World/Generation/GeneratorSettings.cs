using System;

namespace MineCase.Server.World.Generation
{
    public class GeneratorSettings
    {
        public bool GenerateStructure { get; set; } = true;

        public bool UseCaves { get; set; } = true;

        public bool UseRavines { get; set; } = true;

        public bool UseMineShafts { get; set; } = true;

        public bool UseVillages { get; set; } = true;

        public bool UseStrongholds { get; set; } = true;

        public bool UseTemples { get; set; } = true;

        public bool UseMonuments { get; set; } = true;

        public bool UseMansions { get; set; } = true;

        public int SeaLevel { get; set; } = 63;

        public float DepthNoiseScaleX { get; set; } = 200.0F;

        public float DepthNoiseScaleY { get; set; } = 200.0F;

        public float DepthNoiseScaleZ { get; set; } = 200.0F;

        public float CoordinateScale { get; set; } = 0.05F; // mc = 684.412F;

        public float HeightScale { get; set; } = 0.05F; // mc = 684.412F;

        public float MainNoiseScaleX { get; set; } = 80.0F;

        public float MainNoiseScaleY { get; set; } = 160.0F;

        public float MainNoiseScaleZ { get; set; } = 80.0F;

        public float BiomeDepthOffSet { get; set; }

        public float BiomeDepthWeight { get; set; } = 1.0F;

        public float BiomeScaleOffset { get; set; }

        public float BiomeScaleWeight { get; set; } = 1.0F;

        public float BaseSize { get; set; } = 8.5F;

        public float StretchY { get; set; } = 12.0F;

        public float LowerLimitScale { get; set; } = 512.0F;

        public float UpperLimitScale { get; set; } = 512.0F;

        public int BiomeSize { get; set; } = 4;

        public int RiverSize { get; set; } = 4;

        // ores
        public int CoalSize { get; set; } = 17;

        public int CoalCount { get; set; } = 20;

        public int CoalMinHeight { get; set; } = 0;

        public int CoalMaxHeight { get; set; } = 128;

        public int IronSize { get; set; } = 9;

        public int IronCount { get; set; } = 20;

        public int IronMinHeight { get; set; } = 0;

        public int IronMaxHeight { get; set; } = 64;

        public int GoldSize { get; set; } = 9;

        public int GoldCount { get; set; } = 2;

        public int GoldMinHeight { get; set; } = 0;

        public int GoldMaxHeight { get; set; } = 32;

        public int RedstoneSize { get; set; } = 8;

        public int RedstoneCount { get; set; } = 8;

        public int RedstoneMinHeight { get; set; } = 0;

        public int RedstoneMaxHeight { get; set; } = 16;

        public int DiamondSize { get; set; } = 8;

        public int DiamondCount { get; set; } = 1;

        public int DiamondMinHeight { get; set; } = 0;

        public int DiamondMaxHeight { get; set; } = 16;

        public int LapisSize { get; set; } = 7;

        public int LapisCount { get; set; } = 1;

        public int LapisCenterHeight { get; set; } = 16;

        public int LapisSpread { get; set; } = 16;

        public FlatGeneratorInfo FlatGeneratorInfo { get; set; }

        public OverworldGeneratorInfo OverworldGeneratorInfo { get; set; }
    }
}
