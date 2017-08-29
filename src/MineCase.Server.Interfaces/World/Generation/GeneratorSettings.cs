using System;

namespace MineCase.Server.World.Generation
{
    public class GeneratorSettings
    {
        public int Seed { get; set; }

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

        public float CoordinateScale { get; set; } = 684.412F;

        public float HeightScale { get; set; } = 684.412F;

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

        public FlatGeneratorInfo FlatGeneratorInfo { get; set; }

        public OverworldGeneratorInfo OverworldGeneratorInfo { get; set; }
    }
}
