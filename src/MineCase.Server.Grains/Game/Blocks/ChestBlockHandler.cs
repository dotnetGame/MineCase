using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Blocks
{
    [BlockHandler(BlockId.Chest)]
    public class ChestBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public ChestBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        private static readonly (int x, int z)[] _crossCoords = new[]
        {
            (-1, 0), (0, -1), (1, 0), (0, 1)
        };

        public override async Task<bool> CanBeAt(BlockWorldPos position, IGrainFactory grainFactory, IWorld world)
        {
            foreach (var crossCoord in _crossCoords)
            {
                var blockState = await world.GetBlockState(grainFactory, position.X + crossCoord.x, position.Y, position.Z + crossCoord.z);
                if (blockState.Id != (uint)BlockId) continue;

                // 再检查隔壁的隔壁是不是箱子
                foreach (var crossCoord2 in _crossCoords)
                {
                    var blockState2 = await world.GetBlockState(grainFactory, position.X + crossCoord.x + crossCoord2.x, position.Y, position.Z + crossCoord.z + crossCoord2.z);
                    if (blockState2.Id == (uint)BlockId) return false;
                }
            }

            return await base.CanBeAt(position, grainFactory, world);
        }
    }
}
