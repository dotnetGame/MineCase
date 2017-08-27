using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using MineCase.Algorithm;
using MineCase.Algorithm.Noise;


namespace MineCase.Server.World.Generation
{
    [StatelessWorker]
    internal class ChunkGeneratorOverWorldGrain : IChunkGeneratorOverworld
    {
        private float [,,] _densityMap=new float[5,33,5];
        private float [,,] _mainNoiseMap=new float[5,33,5];
        private float [,,] _minLimitMap=new float[5,33,5];
        private float [,,] _maxLimitMap=new float[5,33,5];

        private OctavedNoise<PerlinNoise> noiseGenerator=new OctavedNoise<PerlinNoise>();
        public async Task<ChunkColumn> Generate(int x, int z, GeneratorSettings settings)
        {
            ChunkColumn chunkColumn=new ChunkColumn();
            await GenerateChunk(chunkColumn,x,z,settings);
            await PopulateChunk(chunkColumn,x,z,settings);
            return chunkColumn;
        }

        public async Task GenerateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            Random rand=new Random(settings.seed);
            await GenerateBasicTerrain(chunk, x, z, settings);
            // Todo add biomes blocks

            // Todo genrate structure

            chunk.GenerateSkylightMap();
        }

        public Task PopulateChunk(ChunkColumn chunk, int x, int z, GeneratorSettings settings)
        {
            return Task.CompletedTask;
        }

        private async Task GenerateBasicTerrain(ChunkColumn chunk, int x, int z,GeneratorSettings settings)
        {
            //this.biomesForGeneration = this.world.getBiomeProvider().getBiomesForGeneration(this.biomesForGeneration, x * 4 - 2, z * 4 - 2, 10, 10);
            double [] densityMap=await GenerateDensityMap(x * 4, 0, z * 4,settings);

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
                        double yPart111 = densityMap[zPart11 + yHigh];
                        double yPart121 = densityMap[zPart12 + yHigh];
                        double yPart211 = densityMap[zPart21 + yHigh];
                        double yPart221 = densityMap[zPart22 + yHigh];
                        double yDensityDif11 = (densityMap[zPart11 + yHigh + 1] - yPart111) * 0.125;
                        double yDensityDif12 = (densityMap[zPart12 + yHigh + 1] - yPart121) * 0.125;
                        double yDensityDif21 = (densityMap[zPart21 + yHigh + 1] - yPart211) * 0.125;
                        double yDensityDif22 = (densityMap[zPart22 + yHigh + 1] - yPart221) * 0.125;

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
                                        chunk.setBlockId(posX, posY, posZ, BlockId.Stone);
                                    }
                                    else if (posY < settings.SeaLevel)
                                    {
                                        chunk.setBlockId(posX, posY, posZ, BlockId.Water);
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
        private Task GenerateDensityMap(float[,,] densityMap, int xOffset,int yOffset,int zOffset,GeneratorSettings settings)
        {
            
            noiseGenerator.Noise(_densityMap,
                        new Vector3(xOffset,0,zOffset),
                        new Vector3(settings.DepthNoiseScaleX,1,settings.DepthNoiseScaleZ));

            float coordinateScale = settings.CoordinateScale;
            float heightScale = settings.HeightScale;

            // 生成3个5*5*33的噪声
            noiseGenerator.Noise(_mainNoiseMap,
                        new Vector3(xOffset,yOffset,zOffset),
                        new Vector3(coordinateScale/settings.MainNoiseScaleX,
                                    heightScale/settings.MainNoiseScaleY,
                                    coordinateScale/settings.MainNoiseScaleZ));

            noiseGenerator.Noise(_minLimitMap,
                        new Vector3(xOffset,yOffset,zOffset),
                        new Vector3(coordinateScale,
                                    heightScale,
                                    coordinateScale));

            noiseGenerator.Noise(_maxLimitMap,
                        new Vector3(xOffset,yOffset,zOffset),
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
                    BiomeGenBase centerBiome = this.biomesForGeneration[x1 + 2 + (z1 + 2) * 10];

                    // 求scale和groundYOffset的加权平均值

                    for (int x2 = 0; x2 < 5; ++x2)
                    {
                        for (int z2 = 0; z2 < 5; ++z2)
                        {
                            //BiomeGenBase biome = this.biomesForGeneration[x1 + x2 + (z1 + z2) * 10];
                            //float curGroundYOffset = this.settings.biomeDepthOffSet + biome.minHeight * this.settings.biomeDepthWeight; // biomeDepthOffSet=0
                            float curScale = settings.BiomeScaleOffset + biome.maxScale * settings.BiomeScaleWeight; // biomeScaleOffset=0

                            // parabolicField为 10 / √(该点到中心点的距离^2 + 0.2)
                            float weight = this.parabolicField[x2 + z2 * 5] / (curGroundYOffset + 2.0F);

                            if (biome.minHeight > centerBiome.minHeight)
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

                    float random = _densityMap[xOffset,0,zOffset] / 8000.0F;
                    if(random < 0.0)
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
                        double offset = ((double)y - groundY) * (double)settings.StretchY * 128.0D / 256.0D / _scale; // scale大概在0.1~0.2这样...

                        if (offset < 0.0D)
                        {
                            offset *= 4.0D;
                        }

                        // 并不保证lowerLimit < upperLimit，不过没有影响
                        double lowerLimit = _minLimitMap[x1,y,z1] / (double)settings.LowerLimitScale; // lowerLimitScale=512
                        double upperLimit = _maxLimitMap[x1,y,z1] / (double)settings.UpperLimitScale; // upperLimitScale=512
                        double t = (_mainNoiseMap[x1,y,z1] / 10.0D + 1.0D) / 2.0D;
                        // 这个函数t < 0则取lowerLimit，t > 1则取upperLimit，否则以t为参数在上下限间线性插值
                        double result = MathHelper.denormalizeClamp(lowerLimit, upperLimit,t) - offset;

                        if (y > 29) // y = 30~32
                        {
                            // 在原result和-10之间线性插值，这样y > 240的方块就会越来越少，最后全变成空气
                            double t2 = (double)((float)(y - 29) / 3.0F);
                            result = result * (1.0D - t2) + -10.0D * t2;
                        }

                        _densityMap[x1,y,z1] = (float)result;
                    }
                }
            }
            densityMap=_densityMap;
            return Task.CompletedTask;
        }

        private Task<int> GetDensityMapIndex(int x,int y,int z)
        {
            return Task.FromResult((x*5+z)*33+y);
        }

        private Task<double> GetDensityMapValue(double[] densityMap,int x,int y,int z)
        {
            return Task.FromResult(densityMap[(x*5+z)*33+y]);
        }
    }
}