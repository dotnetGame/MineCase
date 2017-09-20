using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Components;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Items;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class BlockPlacementComponent : Component<EntityGrain>
    {
        public BlockPlacementComponent(string name = "blockPlacement")
            : base(name)
        {
        }

        public async Task PlaceBlock(BlockWorldPos location, EntityInteractHand hand, PlayerDiggingFace face, Vector3 cursorPosition)
        {
            if (face != PlayerDiggingFace.Special)
            {
                var world = AttachedObject.GetWorld();
                var blockState = await world.GetBlockState(GrainFactory, location);
                var blockHandler = BlockHandler.Create((BlockId)blockState.Id);
                if (blockHandler.IsUsable)
                {
                    await blockHandler.UseBy(AttachedObject, GrainFactory, world, location, cursorPosition);
                }
                else
                {
                    var heldItem = await AttachedObject.GetComponent<HeldItemComponent>().GetHeldItem();
                    if (!heldItem.slot.IsEmpty)
                    {
                        var itemHandler = ItemHandler.Create((uint)heldItem.slot.BlockId);
                        if (itemHandler.IsPlaceable)
                        {
                            await itemHandler.PlaceBy(AttachedObject, GrainFactory, world, location, heldItem, face, cursorPosition);
                        }
                    }
                }
            }
        }
    }
}
