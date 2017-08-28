using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using MineCase.Algorithm;
using MineCase.Algorithm.Noise;
using MineCase.Server.World.Biomes;

namespace MineCase.Server.World.Generation
{
    [StatelessWorker]
    internal class ChunkGeneratorOverWorldGrain : Grain, IChunkGeneratorOverworld
    {
        private float [,,] _densityMap=new float[5, 33, 5];
        private float [,,] _depthMap=new float[5, 33, 5];
        private float [,,] _mainNoiseMap=new float[5, 33, 5];
        private float [,,] _minLimitMap=new float[5, 33, 5];
        private float [,,] _maxLimitMap=new float[5, 33, 5];

        private OctavedNoise<PerlinNoise> _depthNoise;
        private OctavedNoise<PerlinNoise> _mainNoise;
        private OctavedNoise<PerlinNoise> _maxNoise;
        private OctavedNoise<PerlinNoise> _minNoise;

        private Random _random;

        private float[,] _biomeWeights;

        private Biome [] _biomesForGeneration; // 10x10

        public override Task OnActivateAsync()
        {
            _densityMap=new float[5, 33, 5];
            _depthMap=new float[5, 33, 5];
            _mainNoiseMap=new float[5, 33, 5];
            _minLimitMap=new float[5, 33, 5];
            _maxLimitMap=new float[5, 33, 5];

            int seed=(int)this.GetPrimaryKeyLong();
            _random=new Random(seed);
            _depthNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 3, 0.5f);
            _mainNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 3, 0.5f);
            _maxNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 3, 0.5f);
            _minNoise = new OctavedNoise<PerlinNoise>(new PerlinNoise(_random.Next()), 3, 0.5f);

            for (int i = -2; i <= 2; ++i)
            {
                for (int j = -2; j <= 2; ++j)
                {
                    float f = 10.0F / (float)Math.Sqrt((i * i + j * j) + 0.2D);
                    _biomeWeights[i + 2, j + 2] = f;
                }
            }
            return Task.CompletedTask;
        }

        public async Task<ChunkColumn> Generate(int x, int z, GeneratorSettings settings)
        {
            ChunkColumn chunkColumn=new ChunkColumn();
            await GenerateChunk(chunkColumn, x, z, settings);
            await PopulateChunk(chunkColumn, x, z, settings);
            return chunkColumn;
        }

        public async Task GenerateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            Random rand=new Random(settings.Seed);
            GetBiomesForGeneration(_biomesForGeneration, x * 4 - 2, z * 4 - 2, 10, 10);
            await GenerateBasicTerrain(chunk, x, z, settings);
            // Todo add biomes blocks

            // Todo genrate structure

            chunk.GenerateSkylightMap();
        }

        public Task PopulateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            return Task.CompletedTask;
        }

        private async Task GenerateBasicTerrain(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            //this.biomesForGeneration = this.world.getBiomeProvider().getBiomesForGeneration(this.biomesForGeneration, x * 4 - 2, z * 4 - 2, 10, 10);
            await GenerateDensityMap(_densityMap, x * 4, 0, z * 4, settings);

            for (int xHigh = 0; xHigh < 4; ++xHigh)
            {
                int xPart1 = xHigh * 5;
                int xPart2 = (xHigh + 1) * 5;

                for (int zHigh = 0; zHigh < 4; ++zHigh)
                {
                    int zPart11 = (xPart1 + zHigh) * 33;
                    int zPart12 = (xPart1 + zHigh + 1) * 33;
                    int zPart21 = (xPart2 + zHigh) * 33;
                    int zPart22 = (xPart2 + zHigh + 1) * 33;

                    for (int yHigh = 0; yHigh < 32; ++yHigh)
                    {
                        double yPart111 = _densityMap[xHigh, yHigh, zHigh  ];
                        double yPart121 = _densityMap[xHigh, yHigh, zHigh+1];
                        double yPart211 = _densityMap[xHigh+1, yHigh, zHigh  ];
                        double yPart221 = _densityMap[xHigh+1, yHigh, zHigh+1];
                        double yDensityDif11 = (_densityMap[xHigh, yHigh+1, zHigh  ] - yPart111) * 0.125;
                        double yDensityDif12 = (_densityMap[xHigh, yHigh+1, zHigh+1] - yPart121) * 0.125;
                        double yDensityDif21 = (_densityMap[xHigh+1, yHigh+1, zHigh  ] - yPart211) * 0.125;
                        double yDensityDif22 = (_densityMap[xHigh+1, yHigh+1, zHigh+1] - yPart221) * 0.125;

                        for (int yLow = 0; yLow < 8; ++yLow)
                        {
                            double density111 = yPart111;
                            double density121 = yPart121;
                            double xDensityDif11 = (yPart211 - yPart111) * 0.25;
                            double xDensityDif21 = (yPart221 - yPart121) * 0.25;

                            for (int xLow = 0; xLow < 4; ++xLow)
                            {
                                double zDensityDif11 = (density121 - density111) * 0.25;
                                double lvt_45_1_ = density111 - zDensityDif11;

                                for (int zLow = 0; zLow < 4; ++zLow)
                                {
                                    int posX=xHigh * 4 + xLow;
                                    int posY=yHigh * 8 + yLow;
                                    int posZ=zHigh * 4 + zLow;
                                    if ((lvt_45_1_ += zDensityDif11) > 0.0)
                                    {
                                        chunk.SetBlockId(posX, posY, posZ, BlockId.Stone);
                                    }
                                    else if (posY < settings.SeaLevel)
                                    {
                                        chunk.SetBlockId(posX, posY, posZ, BlockId.Water);
                                    }
                                }

                                density111 += xDensityDif11;
                                density121 += xDensityDif21;
                            }
                            yPart111 += yDensityDif11;
                            yPart121 += yDensityDif12;
                            yPart211 += yDensityDif21;
                            yPart221 += yDensityDif22;
                        }
                    }
                }
            }
        }
        private Task GenerateDensityMap(float[,,] densityMap, int xOffset, int yOffset, int zOffset, GeneratorSettings settings)
        {

            _depthNoise.Noise(_depthMap,
                        new Vector3(xOffset, 0.0f, zOffset),
                        new Vector3(settings.DepthNoiseScaleX, 1.0f, settings.DepthNoiseScaleZ));

            float coordinateScale = settings.CoordinateScale;
            float heightScale = settings.HeightScale;

            // 生成3个5*5*33的噪声
            _mainNoise.Noise(_mainNoiseMap,
                        new Vector3(xOffset, yOffset, zOffset),
                        new Vector3(coordinateScale/settings.MainNoiseScaleX,
                                    heightScale/settings.MainNoiseScaleY,
                                    coordinateScale/settings.MainNoiseScaleZ));

            _minNoise.Noise(_minLimitMap,
                        new Vector3(xOffset, yOffset, zOffset),
                        new Vector3(coordinateScale,
                                    heightScale,
                                    coordinateScale));

            _maxNoise.Noise(_maxLimitMap,
                        new Vector3(xOffset, yOffset, zOffset),
                        new Vector3(coordinateScale,
                                    heightScale,
                                    coordinateScale));

            //chunk遍历
            for (int x1 = 0; x1 < 5; ++x1)
            {
                for (int z1 = 0; z1 < 5; ++z1)
                {
                    float scale = 0.0F;
                    float groundYOffset = 0.0F;
                    float totalWeight = 0.0F;
                    // 中心点生物群系
                    Biome centerBiome = _biomesForGeneration[x1 + 2 + (z1 + 2) * 10];

                    // 求scale和groundYOffset的加权平均值

                    for (int x2 = 0; x2 < 5; ++x2)
                    {
                        for (int z2 = 0; z2 < 5; ++z2)
                        {
                            Biome biome = _biomesForGeneration[x1 + x2 + (z1 + z2) * 10];
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

                    float random = _densityMap[xOffset, 0, zOffset] / 8000.0F;
                    if (random < 0.0)
                    {
                        random = -random;
                    }
                    else
                    {
                        random = random * 3.0F;
                    }
                    random-=2.0F;

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

                    float _groundYOffset = groundYOffset;
                    float _scale = scale;
                    // groundYOffset有-0.072~0.025的变动量
                    _groundYOffset = _groundYOffset + random * 0.2F;
                    /*_groundYOffset = _groundYOffset * (double)this.settings.baseSize / 8.0D;
                    double groundY = (double)this.settings.baseSize + _groundYOffset * 4.0D;*/
                    // 这个是大概的地面y坐标，实际上也没有保证不会出现浮空岛...
                    float groundY = settings.BaseSize * (1.0F + _groundYOffset / 2.0F); // baseSize=8.5，应该代表了平均地表高度68

                    for (int y = 0; y < 33; ++y) // 注意这个y*8才是最终的y坐标
                    {
                        // result偏移量，这个是负数则趋向固体，是正数则趋向液体和空气
                        float offset = (y - groundY) * settings.StretchY * 128.0F / 256.0F / _scale; // scale大概在0.1~0.2这样...

                        if (offset < 0.0F)
                        {
                            offset *= 4.0F;
                        }

                        // 并不保证lowerLimit < upperLimit，不过没有影响
                        float lowerLimit = _minLimitMap[x1, y, z1] / settings.LowerLimitScale; // lowerLimitScale=512
                        float upperLimit = _maxLimitMap[x1, y, z1] / settings.UpperLimitScale; // upperLimitScale=512
                        float t = (_mainNoiseMap[x1, y, z1] / 10.0F + 1.0F) / 2.0F;
                        // 这个函数t < 0则取lowerLimit，t > 1则取upperLimit，否则以t为参数在上下限间线性插值
                        float result = MathHelper.DenormalizeClamp(lowerLimit, upperLimit, t) - offset;

                        if (y > 29) // y = 30~32
                        {
                            // 在原result和-10之间线性插值，这样y > 240的方块就会越来越少，最后全变成空气
                            float t2 = (float)(y - 29) / 3.0F;
                            result = result * (1.0F - t2) + -10.0F * t2;
                        }

                        _densityMap[x1, y, z1] = (float)result;
                    }
                }
            }
            densityMap=_densityMap;
            return Task.CompletedTask;
        }

        private Task<int> GetDensityMapIndex(int x, int y, int z)
        {
            return Task.FromResult((x*5+z)*33+y);
        }

        private Task<double> GetDensityMapValue(double[] densityMap, int x, int y, int z)
        {
            return Task.FromResult(densityMap[(x*5+z)*33+y]);
        }
    }
}