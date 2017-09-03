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
        public BiomePlains(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "plains";
            _biomeId = BiomeId.Plains;
        }

        // 添加其他东西
        public override async Task Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            float grassColor = (_grassColorNoise.Noise((pos.X + 8) / 200.0F, 0.0F, (pos.Z + 8) / 200.0F) - 0.5F) * 2;

            if (grassColor < -0.8F)
            {
                _flowersPerChunk = 15;
                _grassPerChunk = 5 * 7;
                await GenDoubleFlowers(world, grainFactory, chunk, rand, pos);
                await GenDoubleGrass(world, grainFactory, chunk, rand, pos);
            }
            else
            {
                _flowersPerChunk = 4;
                _grassPerChunk = 10 * 7;
            }

            await GenGrass(world, grainFactory, chunk, rand, pos);
            await GenFlowers(world, grainFactory, chunk, rand, pos);

            int treesPerChunk = _treesPerChunk;

            if (rand.NextDouble() < _extraTreeChance)
            {
                ++treesPerChunk;
            }

            for (int num = 0; num < treesPerChunk; ++num)
            {
                int x = rand.Next(12) + 2;
                int z = rand.Next(12) + 2;

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

        private async Task GenGrass(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            int grassMaxNum = random.Next(_grassPerChunk);
            GrassGenerator generator = new GrassGenerator();
            for (int grassNum = 0; grassNum < grassMaxNum; ++grassNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
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

        private async Task GenFlowers(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            int flowersMaxNum = random.Next(_flowersPerChunk);
            FlowersGenerator generator = new FlowersGenerator();
            for (int flowersNum = 0; flowersNum < flowersMaxNum; ++flowersNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
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

        private async Task GenDoubleFlowers(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            DoubleFlowersGenerator generator = new DoubleFlowersGenerator(PlantsType.Sunflower);
            for (int flowersNum = 0; flowersNum < 10; ++flowersNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
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

        private async Task GenDoubleGrass(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            DoubleGrassGenerator generator = new DoubleGrassGenerator(PlantsType.DoubleTallgrass);
            for (int grassNum = 0; grassNum < 10; ++grassNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
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