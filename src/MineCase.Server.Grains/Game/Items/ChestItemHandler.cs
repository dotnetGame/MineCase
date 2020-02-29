using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Item;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Items
{
    [ItemHandler(BlockId.Chest, 0)]
    public class ChestItemHandler : ItemHandler
    {
        public override bool IsUsable => false;

        public override bool IsPlaceable => true;

        public ChestItemHandler(ItemState item)
            : base(item)
        {
        }

        protected override async Task<BlockState> ConvertToBlock(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, Slot slot)
        {
            int neighborIdx = -1;
            for (int i = 0; i < ChestBlockHandler.CrossCoords.Length; i++)
            {
                var crossCoord = ChestBlockHandler.CrossCoords[i];
                var blockState = await world.GetBlockState(grainFactory, position.X + crossCoord.x, position.Y, position.Z + crossCoord.z);
                if (blockState.Id == (uint)BlockId.Chest)
                {
                    neighborIdx = i;
                    break;
                }
            }

            var yaw = await entity.GetYaw();
            FacingDirectionType facing;
            switch (neighborIdx)
            {
                case 0:
                case 2:
                    // The neighbor is in the X axis, form a X-axis-aligned dblchest:
                    facing = ((yaw >= -90) && (yaw < 90)) ? FacingDirectionType.FacingNorth : FacingDirectionType.FacingSouth;
                    break;
                case 1:
                case 3:
                    // The neighbor is in the Z axis, form a Z-axis-aligned dblchest:
                    facing = (yaw < 0) ? FacingDirectionType.FacingWest : FacingDirectionType.FacingEast;
                    break;
                default:
                    facing = ChestBlockHandler.PlayerYawToFacing(yaw);
                    break;
            }

            return BlockStates.Chest((ChestFacingType)facing, ChestTypeType.Single, ChestWaterloggedType.False);
        }
    }
}
