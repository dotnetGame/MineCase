using System;

namespace MineCase.World.Generation
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

        public float CoordinateScale { get; set; } = 0.05F; // mc = 684.412F;0.05

        public float HeightScale { get; set; } = 0.05F; // mc = 684.412F;0.05

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

        // ores like
        public int DirtSize { get; set; } = 33;

        public int DirtCount { get; set; } = 10;

        public int DirtMinHeight { get; set; } = 0;

        public int DirtMaxHeight { get; set; } = 256;

        public int GravelSize { get; set; } = 33;

        public int GravelCount { get; set; } = 8;

        public int GravelMinHeight { get; set; } = 0;

        public int GravelMaxHeight { get; set; } = 256;

        public int GraniteSize { get; set; } = 33;

        public int GraniteCount { get; set; } = 10;

        public int GraniteMinHeight { get; set; } = 0;

        public int GraniteMaxHeight { get; set; } = 80;

        public int DioriteSize { get; set; } = 33;

        public int DioriteCount { get; set; } = 10;

        public int DioriteMinHeight { get; set; } = 0;

        public int DioriteMaxHeight { get; set; } = 80;

        public int AndesiteSize { get; set; } = 33;

        public int AndesiteCount { get; set; } = 10;

        public int AndesiteMinHeight { get; set; } = 0;

        public int AndesiteMaxHeight { get; set; } = 80;

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

        public BlockState?[] FlatBlockId { get; set; } =
            new BlockState?[] { BlockStates.Bedrock(), BlockStates.Stone(), BlockStates.Dirt(), BlockStates.Grass() };

        // public FlatGeneratorInfo FlatGeneratorInfo { get; set; }

        // public OverworldGeneratorInfo OverworldGeneratorInfo { get; set; }
    }
}
