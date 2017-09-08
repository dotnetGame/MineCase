using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Generation;
using MineCase.Server.World.Plants;
using Orleans;

namespace MineCase.Server.World.Biomes
{
    public class BiomeDesert : Biome
    {
        public BiomeDesert(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "desert";
            _biomeId = BiomeId.Desert;
            _baseHeight = 0.125F;
            _heightVariation = 0.05F;
            _temperature = 2.0F;
            _rainfall = 0.0F;
            _enableRain = false;

            _topBlock = BlockStates.Sand();
            _fillerBlock = BlockStates.Sand();
            _treesPerChunk = -999;
            _deadBushPerChunk = 2;
            _reedsPerChunk = 50;
            _cactiPerChunk = 10;
        }

        // �����������
        public override async Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            await GenCacti(world, grainFactory, chunk, rand, pos);

            // TODO ���������ƺͿ�ľ
            await base.Decorate(world, grainFactory, chunk, rand, pos);
        }

        private async Task GenCacti(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            int cactiMaxNum = random.Next(_cactiPerChunk);

            if (random.Next(64) == 0)
            {
                CactiGenerator generator = new CactiGenerator();
                for (int cactiNum = 0; cactiNum < cactiMaxNum; ++cactiNum)
                {
                    int x = random.Next(14) + 1;
                    int z = random.Next(14) + 1;
                    for (int y = 255; y >= 1; --y)
                    {
                        if (chunk[x, y, z] != BlockStates.Air())
                        {
                            await generator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, y + 1, pos.Z + z));
                            break;
                        }
                    }
                }
            }
        }
    }
}