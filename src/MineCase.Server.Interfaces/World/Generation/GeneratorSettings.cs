using System;

namespace MineCase.Server.World.Generation
{
    public class GeneratorSettings
    {
        public int Seed { get; set; }

        public bool GenerateStructure { get; set; }

        public bool UseCaves { get; set; }

        public bool UseRavines { get; set; }

        public bool UseMineShafts { get; set; }

        public bool UseVillages { get; set; }

        public bool UseStrongholds { get; set; }

        public bool UseTemples { get; set; }

        public bool UseMonuments { get; set; }

        public bool UseMansions { get; set; }

        public int SeaLevel { get; set; }

        public float DepthNoiseScaleX { get; set; }

        public float DepthNoiseScaleY { get; set; }

        public float DepthNoiseScaleZ { get; set; }

        public float CoordinateScale { get; set; }

        public float HeightScale { get; set; }

        public float MainNoiseScaleX { get; set; }

        public float MainNoiseScaleY { get; set; }

        public float MainNoiseScaleZ { get; set; }

        public float BiomeDepthOffSet { get; set; }

        public float BiomeDepthWeight { get; set; }

        public float BiomeScaleOffset { get; set; }

        public float BiomeScaleWeight { get; set; }

        public float BaseSize { get; set; }

        public float StretchY { get; set; }

        public float LowerLimitScale { get; set; }

        public float UpperLimitScale { get; set; }

        public int BiomeSize { get; set; }

        public int RiverSize { get; set; }

        public FlatGeneratorInfo FlatGeneratorInfo { get; set; }

        public OverworldGeneratorInfo OverworldGeneratorInfo { get; set; }
    }
}
