using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MineCase.Algorithm;
using MineCase.Algorithm.Noise;
using MineCase.Algorithm.World;
using MineCase.Algorithm.World.Biomes;
using MineCase.Algorithm.World.Layer;
using MineCase.Algorithm.World.Mine;
using MineCase.World;
using MineCase.World.Generation;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace MineCase.Server.World.Generation
{
    [StatelessWorker]
    internal class ChunkGeneratorOverWorldGrain : Grain, IChunkGeneratorOverworld
    {
        private float[,,] _densityMap;
        private float[,,] _depthMap;
        private float[,,] _mainNoiseMap;
        private float[,,] _minLimitMap;
        private float[,,] _maxLimitMap;
        private float[,,] _surfaceMap;

        private OctavedNoise<PerlinNoise> _depthNoise;
        private OctavedNoise<PerlinNoise> _mainNoise;
        private OctavedNoise<PerlinNoise> _maxNoise;
        private OctavedNoise<PerlinNoise> _minNoise;
        private OctavedNoise<PerlinNoise> _surfaceNoise;

        private int _seed;
        private Random _random;

        private float[,] _biomeWeights;

        private Biome[,] _biomesForGeneration; // 10x10 or 16 x 16

        private GenLayer _genlayer;

        public override Task OnActivateAsync()
        {
            _densityMap = new float[5, 33, 5];
            _depthMap = new float[5, 1, 5];
            _mainNoiseMap = new float[5, 33, 5];
            _minLimitMap = new float[5, 33, 5];
            _maxLimitMap = new float[5, 33, 5];
            _surfaceMap = new float[16, 1, 16];

            _seed = (int)this.GetPrimaryKeyLong();
            _random = new Random(_seed);
            _depthNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 8, 0.5F);
            _mainNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 8, 0.5F);
            _maxNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 8, 0.5F);
            _minNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 8, 0.5F);
            _surfaceNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 8, 0.5F);

            _biomeWeights = new float[5, 5];
            for (int i = -2; i <= 2; ++i)
            {
                for (int j = -2; j <= 2; ++j)
                {
                    float f = 10.0F / (float)Math.Sqrt((i * i + j * j) + 0.2D);
                    _biomeWeights[i + 2, j + 2] = f;
                }
            }

            _biomesForGeneration = new Biome[16, 16];

            _genlayer = GenLayer.InitAllLayer(_seed);

            return Task.CompletedTask;
        }

        public async Task<ChunkColumnCompactStorage> Generate(IWorld world, int x, int z, GeneratorSettings settings)
        {
            var chunkColumn = new ChunkColumnStorage();
            for (int i = 0; i < chunkColumn.Sections.Length; ++i)
                chunkColumn.Sections[i] = new ChunkSectionStorage(true);

            var info = new MapGenerationInfo
            {
                Seed = await world.GetSeed()
            };
            GenerateChunk(info, chunkColumn, x, z, settings);

            // PopulateChunk(world, chunkColumn, x, z, settings);
            return chunkColumn.Compact();
        }

        private void GenerateChunk(MapGenerationInfo info, ChunkColumnStorage chunk, int x, int z, GeneratorSettings settings)
        {
            // 生物群系生成
            // 获取生物群系
            int[,] biomeIds = _genlayer.GetInts(x * 16 - 8, z * 16 - 8, 32, 32);

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    _biomesForGeneration[j, i] = Biome.GetBiome(biomeIds[(int)(0.861111F * j * 4), (int)(0.861111F * i * 4)], settings);
                }
            }

            // 基本地形生成
            GenerateBasicTerrain(chunk, x, z, settings);

            // 获取生物群系
            biomeIds = _genlayer.GetInts(x * 16, z * 16, 16, 16);

            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 16; ++j)
                {
                    _biomesForGeneration[j, i] = Biome.GetBiome(biomeIds[j, i], settings);
                }
            }

            // 设置生物群系
            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 16; ++j)
                {
                    chunk.Biomes[j * 16 + i] = (byte)_biomesForGeneration[j, i].GetBiomeId();
                }
            }

            // 添加生物群系特有方块
            ReplaceBiomeBlocks(settings, x, z, chunk, _biomesForGeneration);

            // Todo genrate structure
            // 生成洞穴
            if (settings.UseCaves)
            {
                CavesGenerator generator = new CavesGenerator(info);
                generator.Generate(info, x, z, chunk, _biomesForGeneration[8, 8]);
            }

            // 计算skylight
            GenerateSkylightMap(chunk);
        }

        public void PopulateChunk(IWorld world, ChunkColumnCompactStorage chunk, int x, int z, GeneratorSettings settings)
        {
            int blockX = x * 16;
            int blockZ = z * 16;
            Biome chunkBiome = Biome.GetBiome(chunk.Biomes[7 * 16 + 7], settings);

            chunkBiome.Decorate(world, GrainFactory, chunk, _random, new BlockWorldPos { X = blockX, Y = 0, Z = blockZ });
            chunkBiome.SpawnMob(world, GrainFactory, chunk, _random, new BlockWorldPos { X = blockX, Y = 0, Z = blockZ });
        }

        private void GenerateBasicTerrain(ChunkColumnStorage chunk, int x, int z, GeneratorSettings settings)
        {
            // 产生高度图
            GenerateDensityMap(_densityMap, x * 4, 0, z * 4, settings);

            // 进行线性插值
            for (int xHigh = 0; xHigh < 4; ++xHigh)
            {
                for (int zHigh = 0; zHigh < 4; ++zHigh)
                {
                    for (int yHigh = 0; yHigh < 32; ++yHigh)
                    {
                        double yPart111 = _densityMap[xHigh, yHigh, zHigh];
                        double yPart121 = _densityMap[xHigh, yHigh, zHigh + 1];
                        double yPart211 = _densityMap[xHigh + 1, yHigh, zHigh];
                        double yPart221 = _densityMap[xHigh + 1, yHigh, zHigh + 1];
                        double yDensityStep11 = (_densityMap[xHigh, yHigh + 1, zHigh] - yPart111) * 0.125;
                        double yDensityStep12 = (_densityMap[xHigh, yHigh + 1, zHigh + 1] - yPart121) * 0.125;
                        double yDensityStep21 = (_densityMap[xHigh + 1, yHigh + 1, zHigh] - yPart211) * 0.125;
                        double yDensityStep22 = (_densityMap[xHigh + 1, yHigh + 1, zHigh + 1] - yPart221) * 0.125;

                        for (int yLow = 0; yLow < 8; ++yLow)
                        {
                            double density111 = yPart111;
                            double density121 = yPart121;
                            double xDensityStep11 = (yPart211 - yPart111) * 0.25;
                            double xDensityStep21 = (yPart221 - yPart121) * 0.25;

                            for (int xLow = 0; xLow < 4; ++xLow)
                            {
                                double zDensityStep11 = (density121 - density111) * 0.25;
                                double blockValue = density111 - zDensityStep11;

                                for (int zLow = 0; zLow < 4; ++zLow)
                                {
                                    int posX = xHigh * 4 + xLow;
                                    int posY = yHigh * 8 + yLow;
                                    int posZ = zHigh * 4 + zLow;
                                    if ((blockValue += zDensityStep11) > 0.0)
                                    {
                                        chunk[posX, posY, posZ] = BlockStates.Stone();
                                    }
                                    else if (posY < settings.SeaLevel)
                                    {
                                        chunk[posX, posY, posZ] = BlockStates.Water();
                                    }
                                    else
                                    {
                                        chunk[posX, posY, posZ] = BlockStates.Air();
                                    }
                                }

                                density111 += xDensityStep11;
                                density121 += xDensityStep21;
                            }

                            yPart111 += yDensityStep11;
                            yPart121 += yDensityStep12;
                            yPart211 += yDensityStep21;
                            yPart221 += yDensityStep22;
                        }
                    }
                }
            }
        }

        private void GenerateDensityMap(float[,,] densityMap, int xOffset, int yOffset, int zOffset, GeneratorSettings settings)
        {
            _depthNoise.Noise(
                _depthMap,
                new Vector3(xOffset + 0.1f, 0.0f, zOffset + 0.1f),
                new Vector3(settings.DepthNoiseScaleX, 1.0f, settings.DepthNoiseScaleZ));

            float coordinateScale = settings.CoordinateScale;
            float heightScale = settings.HeightScale;

            // 生成3个5*5*33的噪声
            _mainNoise.Noise(
                _mainNoiseMap,
                new Vector3(xOffset, yOffset, zOffset),
                new Vector3(
                    coordinateScale / settings.MainNoiseScaleX,
                    heightScale / settings.MainNoiseScaleY,
                    coordinateScale / settings.MainNoiseScaleZ));

            _minNoise.Noise(
                _minLimitMap,
                new Vector3(xOffset, yOffset, zOffset),
                new Vector3(
                    coordinateScale,
                    heightScale,
                    coordinateScale));

            _maxNoise.Noise(
                _maxLimitMap,
                new Vector3(xOffset, yOffset, zOffset),
                new Vector3(
                    coordinateScale,
                    heightScale,
                    coordinateScale));

            // chunk遍历
            for (int x1 = 0; x1 < 5; ++x1)
            {
                for (int z1 = 0; z1 < 5; ++z1)
                {
                    float scale = 0.0F;
                    float groundYOffset = 0.0F;
                    float totalWeight = 0.0F;

                    // 中心点生物群系
                    Biome centerBiome = _biomesForGeneration[z1 + 2, x1 + 2];

                    // 求scale和groundYOffset的加权平均值
                    for (int x2 = 0; x2 < 5; ++x2)
                    {
                        for (int z2 = 0; z2 < 5; ++z2)
                        {
                            Biome biome = _biomesForGeneration[z1 + z2, x1 + x2];
                            float curGroundYOffset = settings.BiomeDepthOffSet + biome.GetBaseHeight() * settings.BiomeDepthWeight; // biomeDepthOffSet=0
                            float curScale = settings.BiomeScaleOffset + biome.GetHeightVariation() * settings.BiomeScaleWeight; // biomeScaleOffset=0

                            // parabolicField为 10 / √(该点到中心点的距离^2 + 0.2)
                            float weight = _biomeWeights[z2, x2] / (curGroundYOffset + 2.0F);

                            if (biome.GetBaseHeight() > centerBiome.GetBaseHeight())
                            {
                                weight /= 2.0F;
                            }

                            scale += curScale * weight;
                            groundYOffset += curGroundYOffset * weight;
                            totalWeight += weight;
                        }
                    }

                    scale = scale / totalWeight;
                    groundYOffset = groundYOffset / totalWeight;
                    scale = scale * 0.9F + 0.1F;
                    groundYOffset = (groundYOffset * 4.0F - 1.0F) / 8.0F;

                    // 取一个-0.36~0.125的随机数，这个随机数决定了起伏的地表
                    float random = (_depthMap[x1, 0, z1] - 0.5F) * 2 / 8000.0F;
                    if (random < 0.0F)
                    {
                        random = -random * 0.3F;
                    }

                    random = random * 3.0F - 2.0F;

                    if (random < 0.0)
                    {
                        random = random / 2.0F;
                        if (random < -1.0)
                        {
                            random = -1.0F;
                        }

                        random = random / 1.4F;
                        random = random / 2.0F;
                    }
                    else
                    {
                        if (random > 1.0F)
                        {
                            random = 1.0F;
                        }

                        random = random / 8.0F;
                    }

                    float groundYOffset1 = groundYOffset;
                    float scale1 = scale;

                    // groundYOffset有-0.072~0.025的变动量
                    groundYOffset1 = groundYOffset1 + random * 0.2F;
                    groundYOffset1 = groundYOffset1 * settings.BaseSize / 8.0F;

                    // 这个是大概的地面y坐标
                    float groundY = settings.BaseSize + groundYOffset1 * 4.0F; // baseSize=8.5，应该代表了平均地表高度68

                    // 注意这个y*8才是最终的y坐标
                    for (int y = 0; y < 33; ++y)
                    {
                        // result偏移量，这个是负数则趋向固体，是正数则趋向液体和空气
                        float offset = (y - groundY) * settings.StretchY * 128.0F / 256.0F / scale1; // scale大概在0.1~0.2这样...

                        if (offset < 0.0F)
                        {
                            offset *= 4.0F;
                        }

                        // 并不保证lowerLimit < upperLimit，不过没有影响
                        float lowerLimit = (_minLimitMap[x1, y, z1] - 0.5F) * 160000 / settings.LowerLimitScale; // lowerLimitScale=512
                        float upperLimit = (_maxLimitMap[x1, y, z1] - 0.5F) * 160000 / settings.UpperLimitScale; // upperLimitScale=512
                        float t = ((_mainNoiseMap[x1, y, z1] - 0.5F) * 160000 / 10.0F + 1.0F) / 2.0F;

                        // 这个函数t < 0则取lowerLimit，t > 1则取upperLimit，否则以t为参数在上下限间线性插值
                        float result = MathHelper.DenormalizeClamp(lowerLimit, upperLimit, t) - offset;

                        // y = 30~32
                        if (y > 29)
                        {
                            // 在原result和-10之间线性插值，这样y > 240的方块就会越来越少，最后全变成空气
                            float t2 = (float)(y - 29) / 3.0F;
                            result = result * (1.0F - t2) + -10.0F * t2;
                        }

                        _densityMap[x1, y, z1] = (float)result;
                    }
                }
            }

            densityMap = _densityMap;
        }

        private void ReplaceBiomeBlocks(GeneratorSettings settings, int x, int z, ChunkColumnStorage chunk, Biome[,] biomesIn)
        {
            _surfaceNoise.Noise(
                _surfaceMap,
                new Vector3(x * 16 + 0.1F, 0, z * 16 + 0.1F),
                new Vector3(0.0625F, 1.0F, 0.0625F));

            for (int x1 = 0; x1 < 16; ++x1)
            {
                for (int z1 = 0; z1 < 16; ++z1)
                {
                    Biome biome = biomesIn[z1, x1];
                    biome.GenerateBiomeTerrain(settings.SeaLevel, _random, chunk, x, z, x1, z1, (_surfaceMap[x1, 0, z1] - 0.5) * 2);
                }
            }
        }

        private void GenerateSkylightMap(ChunkColumnStorage chunk)
        {
            for (int i = 0; i < ChunkConstants.SectionsPerChunk; ++i)
            {
                var skyLight = chunk.Sections[i].SkyLight;
                for (int y = 0; y < ChunkConstants.BlockEdgeWidthInSection; y++)
                {
                    for (int z = 0; z < ChunkConstants.BlockEdgeWidthInSection; z++)
                    {
                        for (int x = 0; x < ChunkConstants.BlockEdgeWidthInSection; x++)
                        {
                            skyLight[x, y, z] = 0xF;
                        }
                    }
                }
            }
        }

        private int GetDensityMapIndex(int x, int y, int z)
        {
            return (x * 5 + z) * 33 + y;
        }

        private double GetDensityMapValue(double[] densityMap, int x, int y, int z)
        {
            return densityMap[(x * 5 + z) * 33 + y];
        }
    }
}