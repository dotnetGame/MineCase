using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Items
{
    [ItemHandler(BlockId.Furnace)]
    public class FurnaceItemHandler : ItemHandler
    {
        public override bool IsUsable => false;

        public override bool IsPlaceable => true;

        public FurnaceItemHandler(uint itemId)
            : base(itemId)
        {
        }

        protected override async Task<BlockState> ConvertToBlock(IPlayer player, IGrainFactory grainFactory, IWorld world, BlockWorldPos position, Slot slot)
        {
            var facing = ChestBlockHandler.PlayerYawToFacing((await player.GetLook()).yaw);
            return BlockStates.Furnace(facing);
        }
    }
}
