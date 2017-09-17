using System;
using MineCase.Algorithm.World.Plants;
using MineCase.Server.World;
using MineCase.World;
using MineCase.World.Biomes;
using MineCase.World.Generation;
using Orleans;

namespace MineCase.Algorithm.World.Biomes
{
    public class BiomeForest : Biome
    {
        public BiomeForest(BiomeProperties properties, GeneratorSettings genSettings)
            : base(properties, genSettings)
        {
            _name = "forest";
            _biomeId = BiomeId.Forest;
            _treesPerChunk = 10;
            _grassPerChunk = 2;

            _temperature = 0.7F;
            _rainfall = 0.8F;
            _enableRain = true;
        }

        // 添加其他东西
        public override void Decorate(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random rand, BlockWorldPos pos)
        {
            float grassColor = (_grassColorNoise.Noise((pos.X + 8) / 200.0F, 0.0F, (pos.Z + 8) / 200.0F) - 0.5F) * 2;

            if (grassColor < -0.8F)
            {
                _flowersPerChunk = 15;
                _grassPerChunk = 5 * 7;
                GenDoubleFlowers(world, grainFactory, chunk, rand, pos);
            }
            else
            {
                _flowersPerChunk = 4;
                _grassPerChunk = 10 * 7;
            }

            GenGrass(world, grainFactory, chunk, rand, pos);
            GenFlowers(world, grainFactory, chunk, rand, pos);
            GenDoubleGrass(world, grainFactory, chunk, rand, pos);

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
                    if (!chunk[x, y, z].IsAir())
                    {
                        h = y + 1;
                        break;
                    }
                }

                treeGenerator.Generate(world, grainFactory, chunk, this, rand, new BlockWorldPos(pos.X + x, h, pos.Z + z));
            }

            base.Decorate(world, grainFactory, chunk, rand, pos);
        }

        private void GenGrass(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            int grassMaxNum = random.Next(_grassPerChunk);
            GrassGenerator generator = new GrassGenerator();
            for (int grassNum = 0; grassNum < grassMaxNum; ++grassNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
                for (int y = 255; y >= 1; --y)
                {
                    if (!chunk[x, y, z].IsAir())
                    {
                        generator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, y + 1, pos.Z + z));
                        break;
                    }
                }
            }
        }

        private void GenFlowers(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            int flowersMaxNum = random.Next(_flowersPerChunk);
            FlowersGenerator generator = new FlowersGenerator();
            for (int flowersNum = 0; flowersNum < flowersMaxNum; ++flowersNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
                for (int y = 255; y >= 1; --y)
                {
                    if (!chunk[x, y, z].IsAir())
                    {
                        generator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, y + 1, pos.Z + z));
                        break;
                    }
                }
            }
        }

        private void GenDoubleFlowers(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            DoubleFlowersGenerator generator = new DoubleFlowersGenerator(PlantsType.Sunflower);
            for (int flowersNum = 0; flowersNum < 10; ++flowersNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
                for (int y = 255; y >= 1; --y)
                {
                    if (!chunk[x, y, z].IsAir())
                    {
                        generator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, y + 1, pos.Z + z));
                        break;
                    }
                }
            }
        }

        private void GenDoubleGrass(IWorld world, IGrainFactory grainFactory, ChunkColumnStorage chunk, Random random, BlockWorldPos pos)
        {
            DoubleGrassGenerator generator = new DoubleGrassGenerator(PlantsType.DoubleTallgrass);
            for (int grassNum = 0; grassNum < 2; ++grassNum)
            {
                int x = random.Next(16);
                int z = random.Next(16);
                for (int y = 255; y >= 1; --y)
                {
                    if (!chunk[x, y, z].IsAir())
                    {
                        generator.Generate(world, grainFactory, chunk, this, random, new BlockWorldPos(pos.X + x, y + 1, pos.Z + z));
                        break;
                    }
                }
            }
        }
    }
}