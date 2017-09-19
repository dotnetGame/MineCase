using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Windows;
using MineCase.Server.World;
using MineCase.World;
using Orleans;
using MineCase.Server.Game.Entities.Components;

namespace MineCase.Server.Game.Blocks
{
    [BlockHandler(BlockId.CraftingTable)]
    public sealed class CraftingTableBlockHandler : BlockHandler
    {
        public CraftingTableBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        public override bool IsUsable => true;

        public override async Task UseBy(IPlayer player, IGrainFactory grainFactory, IWorld world, BlockWorldPos blockPosition, Vector3 cursorPosition)
        {
            var window = grainFactory.GetGrain<ICraftingWindow>(Guid.NewGuid());
            await player.Tell(new OpenWindow { Window = window });
        }
    }
}
