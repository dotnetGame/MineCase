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
        private GrassGenerator _grassGenerator;

        private FlowersGenerator _flowersGenerator;

        public BiomeDesert(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "desert";
            _biomeId = BiomeId.Desert;

            _topBlock = BlockStates.Sand();
            _fillerBlock = BlockStates.Sand();
            _treesPerChunk = -999;
            _deadBushPerChunk = 2;
            _reedsPerChunk = 50;
            _cactiPerChunk = 10;

            _grassGenerator = new GrassGenerator();
            _flowersGenerator = new FlowersGenerator();
        }

        // �����������
        public override async Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            // TODO ���������ƺͿ�ľ
            await base.Decorate(world, grainFactory, chunk, rand, pos);
        }
    }
}