using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Algorithm.Noise;
using MineCase.Server.World.Generation;
using MineCase.Server.World.Mine;
using MineCase.Server.World.Plants;
using Orleans;

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
    }

    public abstract class Biome
    {
        // Biome有关的生成器的设置
        private GeneratorSettings _genSettings;

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
        public BlockState _topBlock = BlockStates.GrassBlock();
        /** The block to fill spots in when not on the top */
        public BlockState _fillerBlock = BlockStates.Dirt();

        // 噪声函数
        protected static readonly OctavedNoise<PerlinNoise> _temperatureNoise =
            new OctavedNoise<PerlinNoise>(new PerlinNoise(1234), 4, 0.5F);

        protected static readonly OctavedNoise<PerlinNoise> _grassColorNoise =
            new OctavedNoise<PerlinNoise>(new PerlinNoise(2345), 4, 0.5F);

        // 矿物生成器
        private MinableGenerator _dirtGen; // 你没看错，这些当作矿物生成
        private MinableGenerator _gravelOreGen;
        private MinableGenerator _graniteGen;
        private MinableGenerator _dioriteGen;
        private MinableGenerator _andesiteGen;

        private MinableGenerator _coalGen;
        private MinableGenerator _ironGen;
        private MinableGenerator _goldGen;
        private MinableGenerator _redstoneGen;
        private MinableGenerator _diamondGen;
        private MinableGenerator _lapisGen;

        public Biome(BiomeProperties properties, GeneratorSettings genSettings)
        {
            _genSettings = genSettings;

            _name = properties.BiomeName;
            _baseHeight = properties.BaseHeight;
            _heightVariation = properties.HeightVariation;
            _temperature = properties.Temperature;
            _rainfall = properties.Rainfall;
            _waterColor = properties.WaterColor;
            _enableSnow = properties.EnableSnow;
            _enableRain = properties.EnableRain;

            _dirtGen = new MinableGenerator(
                BlockStates.Dirt(),
                genSettings.DirtSize,
                genSettings.DirtMaxHeight,
                genSettings.DirtMinHeight);
            _gravelOreGen = new MinableGenerator(
                BlockStates.Gravel(),
                genSettings.GravelSize,
                genSettings.GravelMaxHeight,
                genSettings.GravelMinHeight);
            _graniteGen = new MinableGenerator(
                BlockStates.Stone(StoneType.Granite),
                genSettings.GraniteSize,
                genSettings.GraniteMaxHeight,
                genSettings.GraniteMinHeight);
            _dioriteGen = new MinableGenerator(
                BlockStates.Stone(StoneType.Diorite),
                genSettings.DioriteSize,
                genSettings.DioriteMaxHeight,
                genSettings.DioriteMinHeight);
            _andesiteGen = new MinableGenerator(
                BlockStates.Stone(StoneType.Andesite),
                genSettings.AndesiteSize,
                genSettings.AndesiteMaxHeight,
                genSettings.AndesiteMinHeight);
            _coalGen = new MinableGenerator(
                BlockStates.CoalOre(),
                genSettings.CoalSize,
                genSettings.CoalMaxHeight,
                genSettings.CoalMinHeight);
            _ironGen = new MinableGenerator(
                BlockStates.IronOre(),
                genSettings.IronSize,
                genSettings.IronMaxHeight,
                genSettings.IronMinHeight);
            _goldGen = new MinableGenerator(
                BlockStates.GoldOre(),
                genSettings.GoldSize,
                genSettings.GoldMaxHeight,
                genSettings.GoldMinHeight);
            _redstoneGen = new MinableGenerator(
                BlockStates.RedstoneOre(),
                genSettings.RedstoneSize,
                genSettings.RedstoneMaxHeight,
                genSettings.RedstoneMinHeight);
            _diamondGen = new MinableGenerator(
                BlockStates.DiamondOre(),
                genSettings.DiamondSize,
                genSettings.DiamondMaxHeight,
                genSettings.DiamondMinHeight);
            _lapisGen = new MinableGenerator(
                BlockStates.LapisLazuliOre(),
                genSettings.LapisSize,
                genSettings.LapisCenterHeight + genSettings.LapisSpread,
                genSettings.LapisCenterHeight - genSettings.LapisSpread);
        }

        public float GetBaseHeight()
        {
            return _baseHeight;
        }

        public float GetHeightVariation()
        {
            return _heightVariation;
        }

        public static Biome GetBiome(int id, GeneratorSettings settings)
        {
            BiomeId biomeId = (BiomeId)id;
            switch (biomeId)
            {
                case BiomeId.Ocean:
                // return new BiomeOcean();
                case BiomeId.Plains:
                    return new BiomePlains(new BiomeProperties { BiomeName = "plains" }, settings);
                case BiomeId.Desert:
                // return new BiomeDesert();
                case BiomeId.ExtremeHills:
                // return new BiomeExtremeHills();
                default:
                    return null;
            }
        }

        // 随机获得一个该生物群系可能出现的草
        public virtual PlantsType GetRandomGrass(Random rand)
        {
            return PlantsType.TallGrass;
        }

        // 随机获得一个该生物群系可能出现的花
        public virtual PlantsType GetRandomFlower(Random rand)
        {
            double n = rand.NextDouble();
            if (n > 0.5)
            {
                return PlantsType.RedFlower;
            }
            else
            {
                return PlantsType.YellowFlower;
            }
        }

        // 后期添加一些方块，Biome基类主要生成矿物
        public virtual Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            _dirtGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.DirtCount);
            _gravelOreGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.GravelCount);
            _graniteGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.GraniteCount);
            _dioriteGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.DioriteCount);
            _andesiteGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.AndesiteCount);

            _coalGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.CoalCount);
            _ironGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.IronCount);
            _goldGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.GoldCount);
            _redstoneGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.RedstoneCount);
            _diamondGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.DiamondCount);
            _lapisGen.Generate(world, grainFactory, chunk, rand, pos, _genSettings.LapisCount);
            return Task.CompletedTask;
        }

        // 产生生物群系特有的方块
        public void GenerateBiomeTerrain(int seaLevel, Random rand, ChunkColumnStorage chunk, int chunk_x, int chunk_z, int x_in_chunk, int z_in_chunk, double noiseVal)
        {
            BlockState topBlockstate = _topBlock;
            BlockState fillerBlockstate = _fillerBlock;
            int surfaceFlag = -1;
            int surfaceDepth = (int)(noiseVal / 3.0D + 3.0D + rand.NextDouble() * 0.25D);

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