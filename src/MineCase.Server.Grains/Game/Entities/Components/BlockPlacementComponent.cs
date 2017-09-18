using MineCase.Engine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using MineCase.World;
using MineCase.Server.Components;
using MineCase.Server.World;
using MineCase.Server.Game.Blocks;
using MineCase.Server.Game.Items;

namespace MineCase.Server.Game.Entities.Components
{
    internal class BlockPlacementComponent : Component<PlayerGrain>
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
                    var heldItem = await AttachedObject.GetHeldItem();
                    if (!heldItem.slot.IsEmpty)
                    {
                        var itemHandler = ItemHandler.Create((uint)heldItem.slot.BlockId);
                        if (itemHandler.IsPlaceable)
                        {
                            var inventory = AttachedObject.GetComponent<InventoryComponent>().GetInventoryWindow();
                            await itemHandler.PlaceBy(AttachedObject, GrainFactory, world, location, inventory, heldItem.index, face, cursorPosition);
                        }
                    }
                }
            }
        }
    }
}
