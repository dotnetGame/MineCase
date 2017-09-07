using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows;
using MineCase.Server.World;
using Orleans;

namespace MineCase.Server.Game.Items
{
    [ItemHandler(BlockId.Chest)]
    public class ChestItemHandler : ItemHandler
    {
        public override bool IsUsable => false;

        public override bool IsPlaceable => true;

        public ChestItemHandler(uint itemId)
            : base(itemId)
        {
        }

        protected override Task<BlockState> ConvertToBlock(IPlayer player, Slot slot)
        {
            return base.ConvertToBlock(player, slot);
        }
    }
}
