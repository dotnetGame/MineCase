using System;
using MineCase.Algorithm.World.Biomes;
using MineCase.Block;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Algorithm.World.Plants
{
    public class CactiGenerator : PlantsGenerator
    {
        private int _minCactiHeight;

        public CactiGenerator(int cactiHeight = 2)
        {
            _minCactiHeight = cactiHeight;
        }

        public bool CanCactiGrow(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos, int height)
        {
            bool result = true;

            // 检查所有方块可替换
            for (int y = pos.Y; y <= pos.Y + 1 + height; ++y)
            {
                int xzSize = 1;

                // 检查这个平面所有方块可替换
                for (int x = pos.X - xzSize; x <= pos.X + xzSize && result; ++x)
                {
                    for (int z = pos.Z - xzSize; z <= pos.Z + xzSize && result; ++z)
                    {
                        if (y >= 0 && y < 256)
                        {
                            BlockChunkPos chunkPos = new BlockWorldPos(x, y, z).ToBlockChunkPos();
                            BlockState state = chunk[chunkPos.X, chunkPos.Y, chunkPos.Z];
                            if (!state.IsAir())
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        public static bool CanSustainCacti(BlockState state)
        {
            if (state == BlockStates.Sand())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Generate(IWorld world, IGrainFactory grainFactory, ChunkColumnCompactStorage chunk, Biome biome, Random random, BlockWorldPos pos)
        {
            int height = random.Next(3) + _minCactiHeight;
            BlockChunkPos chunkPos = pos.ToBlockChunkPos();

            // 不超出世界边界
            if (pos.Y >= 1 && pos.Y + height + 1 <= 256)
            {
                if (CanCactiGrow(world, grainFactory, chunk, biome, random, pos, height))
                {
                    BlockState state = chunk[chunkPos.X, chunkPos.Y - 1, chunkPos.Z];
                    if (CanSustainCacti(state))
                    {
                        for (int y = 0; y < height; ++y)
                        {
                            chunk[chunkPos.X, chunkPos.Y + y, chunkPos.Z] = BlockStates.Cactus(CactusAgeType.Age15);
                        }
                    }
                }
            }
        }
    }
}
