using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.World.Biomes
{
    public class BiomeProperties
    {
        public string BiomeName { get; set; }

        public float BaseHeight { get; set; } = 0.1F;

        public float HeightVariation { get; set; } = 0.2F;

        public float Temperature { get; set; } = 0.5F;

        public float Rainfall { get; set; } = 0.5F;

        public int WaterColor { get; set; } = 16777215;

        public bool EnableSnow { get; set; } = false;

        public bool EnableRain { get; set; } = true;

        public BiomeProperties(string name)
        {
            BiomeName = name;
        }
    }

    public abstract class Biome
    {
        private string _name;
        /** The base height of this biome. Default 0.1. */
        private float _baseHeight;
        /** The variation from the base height of the biome. Default 0.3. */
        private float _heightVariation;
        /** The temperature of this biome. */
        private float _temperature;
        /** The rainfall in this biome. */
        private float _rainfall;
        /** Color tint applied to water depending on biome */
        private int _waterColor;
        /** Set to true if snow is enabled for this biome. */
        private bool _enableSnow;
        /** Is true (default) if the biome support rain (desert and nether can't have rain) */
        private bool _enableRain;
        /** The block expected to be on the top of this biome */
        public BlockState _topBlock = BlockStates.Grass();
        /** The block to fill spots in when not on the top */
        public BlockState _fillerBlock = BlockStates.Dirt();

        public Biome(BiomeProperties properties)
        {
            _name = properties.BiomeName;
            _baseHeight = properties.BaseHeight;
            _heightVariation = properties.HeightVariation;
            _temperature = properties.Temperature;
            _rainfall = properties.Rainfall;
            _waterColor = properties.WaterColor;
            _enableSnow = properties.EnableSnow;
            _enableRain = properties.EnableRain;
        }

        public float GetBaseHeight()
        {
            return _baseHeight;
        }

        public float GetHeightVariation()
        {
            return _heightVariation;
        }

        public static Biome GetBiome(int id)
        {
            BiomeId biomeId = (BiomeId)id;
            switch (biomeId)
            {
                case BiomeId.Ocean:
                    // return new BiomeOcean();
                case BiomeId.Plains:
                    // return new BiomePlains();
                case BiomeId.Desert:
                    // return new BiomeDesert();
                case BiomeId.ExtremeHills:
                    // return new BiomeExtremeHills();
                default:
                    return null;
            }
        }

        public void GenerateBiomeTerrain(int seaLevel, Random rand, ChunkColumnStorage chunk, int x, int z, double noiseVal)
        {
            BlockState topBlockstate = _topBlock;
            BlockState fillerBlockstate = _fillerBlock;
            int surfaceFlag = -1;
            int surfaceDepth = (int)(noiseVal / 3.0D + 3.0D + rand.NextDouble() * 0.25D);
            int x_in_chunk = x % 16;
            int z_in_chunk = z % 16;

            for (int y = 255; y >= 0; --y)
            {
                if (y <= rand.Next(5))
                {
                    chunk[x_in_chunk, y, z_in_chunk] = BlockStates.Bedrock();
                }
                else
                {
                    BlockState iblockstate = chunk[x_in_chunk, y, z_in_chunk];

                    if (iblockstate == BlockStates.Air())
                    {
                        surfaceFlag = -1;
                    }
                    else if (iblockstate == BlockStates.Stone())
                    {
                        // 将地面石头进行生物群系替换
                        if (surfaceFlag == -1)
                        {
                            if (surfaceDepth <= 0)
                            {
                                topBlockstate = BlockStates.Air();
                                fillerBlockstate = BlockStates.Stone();
                            }
                            else if (y >= seaLevel - 4 && y <= seaLevel + 1)
                            {
                                topBlockstate = _topBlock;
                                fillerBlockstate = _fillerBlock;
                            }

                            // TODO 根据温度变化决定水的状态
                            surfaceFlag = surfaceDepth;

                            if (y >= seaLevel - 1)
                            {
                                chunk[x_in_chunk, y, z_in_chunk] = topBlockstate;
                            }
                            else if (y < seaLevel - 7 - surfaceDepth)
                            {
                                topBlockstate = BlockStates.Air();
                                fillerBlockstate = BlockStates.Stone();
                                chunk[x_in_chunk, y, z_in_chunk] = BlockStates.Gravel();
                            }
                            else
                            {
                                chunk[x_in_chunk, y, z_in_chunk] = fillerBlockstate;
                            }
                        }
                        else if (surfaceFlag > 0)
                        {
                            --surfaceFlag;
                            chunk[x_in_chunk, y, z_in_chunk] = fillerBlockstate;
                        }
                    }
                }
            }
        }
    }

    public enum BiomeId : byte
    {
        Ocean = 0,
        Plains = 1,
        Desert = 2,
        ExtremeHills = 3
    }
}