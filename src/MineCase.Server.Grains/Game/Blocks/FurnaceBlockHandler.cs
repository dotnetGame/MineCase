﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Server.Game.BlockEntities;
using MineCase.Server.Game.Entities;
using MineCase.Server.World;
using MineCase.World;
using Orleans;

namespace MineCase.Server.Game.Blocks
{
    [BlockHandler(BlockId.Furnace)]
    [BlockHandler(BlockId.BurningFurnace)]
    public class FurnaceBlockHandler : BlockHandler
    {
        public override bool IsUsable => true;

        public FurnaceBlockHandler(BlockId blockId)
            : base(blockId)
        {
        }

        public override async Task UseBy(IEntity entity, IGrainFactory grainFactory, IWorld world, BlockWorldPos blockPosition, Vector3 cursorPosition)
        {
            var blockEntity = (await world.GetBlockEntity(grainFactory, blockPosition)).Cast<IFurnaceBlockEntity>();
            await blockEntity.Tell(new UseBy { Entity = entity });
        }
    }
}
