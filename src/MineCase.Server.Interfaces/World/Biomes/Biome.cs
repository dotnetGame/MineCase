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

        public BiomeId BiomeId { get; set; }

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
        protected GeneratorSettings _genSettings;

        protected string _name;

        protected BiomeId _biomeId;
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

        // 植被设置
        protected int _treesPerChunk;
        protected float _extraTreeChance;
        protected int _grassPerChunk;
        protected int _flowersPerChunk;

        protected int _deadBushPerChunk;
        protected int _reedsPerChunk;
        protected int _cactiPerChunk;

        public Biome(BiomeProperties properties, GeneratorSettings genSettings)
        {
            _genSettings = genSettings;

            _name = properties.BiomeName;
            _biomeId = properties.BiomeId;
            _baseHeight = properties.BaseHeight;
            _heightVariation = properties.HeightVariation;
            _temperature = properties.Temperature;
            _rainfall = properties.Rainfall;
            _waterColor = properties.WaterColor;
            _enableSnow = properties.EnableSnow;
            _enableRain = properties.EnableRain;

            _dirtGen = new MinableGenerator(
                BlockStates.Dirt(),
                genSettings.DirtSize);
            _gravelOreGen = new MinableGenerator(
                BlockStates.Gravel(),
                genSettings.GravelSize);
            _graniteGen = new MinableGenerator(
                BlockStates.Stone(StoneType.Granite),
                genSettings.GraniteSize);
            _dioriteGen = new MinableGenerator(
                BlockStates.Stone(StoneType.Diorite),
                genSettings.DioriteSize);
            _andesiteGen = new MinableGenerator(
                BlockStates.Stone(StoneType.Andesite),
                genSettings.AndesiteSize);
            _coalGen = new MinableGenerator(
                BlockStates.CoalOre(),
                genSettings.CoalSize);
            _ironGen = new MinableGenerator(
                BlockStates.IronOre(),
                genSettings.IronSize);
            _goldGen = new MinableGenerator(
                BlockStates.GoldOre(),
                genSettings.GoldSize);
            _redstoneGen = new MinableGenerator(
                BlockStates.RedstoneOre(),
                genSettings.RedstoneSize);
            _diamondGen = new MinableGenerator(
                BlockStates.DiamondOre(),
                genSettings.DiamondSize);
            _lapisGen = new MinableGenerator(
                BlockStates.LapisLazuliOre(),
                genSettings.LapisSize);

            _treesPerChunk = 0; // mc 0
            _extraTreeChance = 0.05F; // mc 0.05F
            _grassPerChunk = 10;
            _flowersPerChunk = 4;

            _deadBushPerChunk = 2;
            _reedsPerChunk = 50;
            _cactiPerChunk = 10;
        }

        public BiomeId GetBiomeId()
        {
            return _biomeId;
        }

        public string GetBiomeName()
        {
            return _name;
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
                    return new BiomePlains(new BiomeProperties(), settings);
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

        // 随机获得一个该生物群系可能出现的树
        public virtual PlantsType GetRandomTree(Random rand)
        {
            int n = rand.Next(3);
            switch (n)
            {
                case 0:
                    return PlantsType.Oak;
                case 1:
                    return PlantsType.Spruce;
                case 2:
                    return PlantsType.Birch;
                default:
                    return PlantsType.Oak;
            }
        }

        public void GenerateOre(MinableGenerator generator, IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos position, int count, int minHeight, int maxHeight)
        {
            if (minHeight > maxHeight)
            {
                int tmp = minHeight;
                minHeight = maxHeight;
                maxHeight = tmp;
            }
            else if (maxHeight == minHeight)
            {
                if (minHeight < 255)
                    ++maxHeight;
                else
                    --minHeight;
            }

            for (int j = 0; j < count; ++j)
            {
                BlockWorldPos blockpos = BlockWorldPos.Add(
                    position,
                    random.Next(16),
                    random.Next(maxHeight - minHeight) + minHeight,
                    random.Next(16));
                generator.Generate(world, grainFactory, chunk, random, blockpos);
            }
        }

        // 后期添加一些方块，Biome基类主要生成矿物
        public virtual Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            GenerateOre(_dirtGen, world, grainFactory, chunk, rand, pos, _genSettings.DirtCount, _genSettings.DirtMaxHeight, _genSettings.DirtMinHeight);
            GenerateOre(_gravelOreGen, world, grainFactory, chunk, rand, pos, _genSettings.GravelCount, _genSettings.GravelMaxHeight, _genSettings.GravelMinHeight);
            GenerateOre(_graniteGen, world, grainFactory, chunk, rand, pos, _genSettings.GraniteCount, _genSettings.GraniteMaxHeight, _genSettings.GraniteMinHeight);
            GenerateOre(_dioriteGen, world, grainFactory, chunk, rand, pos, _genSettings.DioriteCount, _genSettings.DioriteMaxHeight, _genSettings.DioriteMinHeight);
            GenerateOre(_andesiteGen, world, grainFactory, chunk, rand, pos, _genSettings.AndesiteCount, _genSettings.AndesiteMaxHeight, _genSettings.AndesiteMinHeight);

            GenerateOre(_coalGen, world, grainFactory, chunk, rand, pos, _genSettings.CoalCount, _genSettings.CoalMaxHeight, _genSettings.CoalMinHeight);
            GenerateOre(_ironGen, world, grainFactory, chunk, rand, pos, _genSettings.IronCount, _genSettings.IronMaxHeight, _genSettings.IronMinHeight);
            GenerateOre(_goldGen, world, grainFactory, chunk, rand, pos, _genSettings.GoldCount, _genSettings.GoldMaxHeight, _genSettings.GoldMinHeight);
            GenerateOre(_redstoneGen, world, grainFactory, chunk, rand, pos, _genSettings.RedstoneCount, _genSettings.RedstoneMaxHeight, _genSettings.RedstoneMinHeight);
            GenerateOre(_diamondGen, world, grainFactory, chunk, rand, pos, _genSettings.DiamondCount, _genSettings.DiamondMaxHeight, _genSettings.DiamondMinHeight);
            GenerateOre(_lapisGen, world, grainFactory, chunk, rand, pos, _genSettings.LapisCount, _genSettings.LapisCenterHeight + _genSettings.LapisSpread, _genSettings.LapisCenterHeight - _genSettings.LapisSpread);
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