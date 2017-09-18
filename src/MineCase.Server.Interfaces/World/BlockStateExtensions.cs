using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.World;
using Orleans;

namespace MineCase.Server.World
{
    public static class BlockStateExtensions
    {
        // 一些特性
        public static float IsLightOpacity(this BlockState state)
        {
            if (state.Id == (uint)BlockId.Glass)
            {
                return 1.0F;
            }
            else
            {
                return 0.0F;
            }
        }

        public static bool IsAir(this BlockState state)
        {
            return state.Id == (uint)BlockId.Air;
        }

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

        public static uint ToUInt32(this BlockState blockState)
        {
            return ChunkSectionCompactStorage.ToUInt32(ref blockState);
        }
    }
}
