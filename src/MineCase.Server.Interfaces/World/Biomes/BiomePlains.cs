using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World.Generation;
using MineCase.Server.World.Plants;
using Orleans;

namespace MineCase.Server.World.Biomes
{
    public class BiomePlains : Biome
    {
        private GrassGenerator _grassGenerator;

        private FlowersGenerator _flowersGenerator;

        public BiomePlains(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _grassGenerator = new GrassGenerator();
            _flowersGenerator = new FlowersGenerator();
        }

        // 添加其他东西
        public override async Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            float d0 = (_grassColorNoise.Noise((pos.X + 8) / 200.0F, 0.0F, (pos.Z + 8) / 200.0F) - 0.5F) * 2;

            if (d0 < -0.8F)
            {
                _flowersGenerator.FlowersPerChunk = 15;
                _grassGenerator.GrassPerChunk = 5 * 7;
            }
            else
            {
                _flowersGenerator.FlowersPerChunk = 4;
                _grassGenerator.GrassPerChunk = 10 * 7;
            }

            await _grassGenerator.Generate(world, grainFactory, chunk, this, rand, pos);
            await _flowersGenerator.Generate(world, grainFactory, chunk, this, rand, pos);

            int treesPerChunk = _treesPerChunk;

            if (rand.NextDouble() < _extraTreeChance)
            {
                ++treesPerChunk;
            }

            for (int num = 0; num < treesPerChunk; ++num)
            {
                int x = rand.Next(8) + 4;
                int z = rand.Next(8) + 4;

                TreeGenerator treeGenerator = new TreeGenerator(5, false, GetRandomTree(rand));

                // 获得地表面高度
                int h = 0;
                for (int y = 255; y >= 0; --y)
                {
                    if (chunk[x, y, z] != BlockStates.Air())
                    {
                        h = y + 1;
                        break;
                    }
                }

                await treeGenerator.Generate(world, grainFactory, chunk, this, rand, new BlockWorldPos(pos.X + x, h, pos.Z + z));
            }

            await base.Decorate(world, grainFactory, chunk, rand, pos);
        }
    }
}