using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Block
{
    public static class BlockStateExtensions
    {
        // 一些特性
        public static int IsLightOpacity(this BlockState state)
        {
            if (state.IsSameId(BlockStates.Air()))
            {
                return 255;
            }
            else if (state.IsSameId(BlockStates.Water()))
            {
                return 255;
            }
            else if (state.IsSameId(BlockStates.Lava()))
            {
                return 255;
            }
            else if (state.IsSameId(BlockStates.Glass()))
            {
                return 255;
            }
            else if (state.IsSameId(BlockStates.Grass()))
            {
                return 255;
            }
            else if (state.IsSameId(BlockStates.Poppy()))
            {
                return 255;
            }
            else if (state.IsSameId(BlockStates.Dandelion()))
            {
                return 255;
            }

            /*
            else if (state.IsSameId(BlockStates.LargeFlowers()))
            {
                return 255;
            }
            */
            else
            {
                return 0;
            }
        }

        public static bool IsAir(this BlockState state)
        {
            return state.Id == (uint)BlockId.Air;
        }

        public static bool IsLeaves(this BlockState state)
        {
            return state.IsId(BlockId.AcaciaLeaves)
                || state.IsId(BlockId.BirchLeaves)
                || state.IsId(BlockId.DarkOakLeaves)
                || state.IsId(BlockId.JungleLeaves)
                || state.IsId(BlockId.OakLeaves)
                || state.IsId(BlockId.JungleLeaves);
        }

        public static bool IsWood(this BlockState state)
        {
            return state.IsId(BlockId.AcaciaLog)
                || state.IsId(BlockId.BirchLog)
                || state.IsId(BlockId.DarkOakLog)
                || state.IsId(BlockId.JungleLog)
                || state.IsId(BlockId.OakLog)
                || state.IsId(BlockId.SpruceLog)
                || state.IsId(BlockId.StrippedAcaciaLog)
                || state.IsId(BlockId.StrippedBirchLog)
                || state.IsId(BlockId.StrippedDarkOakLog)
                || state.IsId(BlockId.StrippedJungleLog)
                || state.IsId(BlockId.StrippedOakLog)
                || state.IsId(BlockId.StrippedSpruceLog);
        }

        public static bool CanMobStand(this BlockState state)
        {
            return !state.IsSameId(BlockStates.Air()) &&
                !state.IsSameId(BlockStates.Grass()) &&
                !state.IsSameId(BlockStates.Water()) &&

                // !state.IsSameId(BlockStates.LargeFlowers()) &&
                !state.IsSameId(BlockStates.Poppy()) &&
                !state.IsSameId(BlockStates.Dandelion());
        }

        public static bool IsMobCollided(this BlockState state)
        {
            return !state.IsSameId(BlockStates.Air()) &&
                !state.IsSameId(BlockStates.Grass()) &&

                // !state.IsSameId(BlockStates.LargeFlowers()) &&
                !state.IsSameId(BlockStates.Poppy()) &&
                !state.IsSameId(BlockStates.Dandelion());
        }

        /*
        public static async Task<bool> CanBlockStay(this BlockState state, IWorld world, IGrainFactory grainFactory, int x, int y, int z)
        {
            if (state.IsSameId(BlockStates.Grass()))
            {
                if (y > 0)
                {
                    var downState = await world.GetBlockState(grainFactory, x, y - 1, z);
                    if (downState == BlockStates.Dirt() ||
                        downState == BlockStates.Grass())
                    {
                        return true;
                    }
                }

                return false;
            }
            else
            {
                return true;
            }
        }
        */

        public static uint ToUInt32(this BlockState blockState)
        {
            return ChunkSectionCompactStorage.ToUInt32(ref blockState);
        }
    }
}
