using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MineCase.Block;
using MineCase.Engine;
using MineCase.Item;
using MineCase.Server.Components;
using MineCase.Server.Game.Items;
using MineCase.Server.World;
using MineCase.World;

namespace MineCase.Server.Game.Entities.Components
{
    internal class DiggingComponent : Component<EntityGrain>
    {
        public const float MaxDiggingRadius = 6;

        private long _diggingStartTick;
        private (BlockWorldPos, BlockState)? _diggingBlock;

        public DiggingComponent(string name = "digging")
            : base(name)
        {
        }

        public async Task StartDigging(BlockWorldPos location, PlayerDiggingFace face)
        {
            var playerPos = AttachedObject.GetEntityWorldPosition();
            var gameMode = AttachedObject.GetValue(GameModeComponent.GameModeProperty);
            var world = AttachedObject.GetWorld();

            // A Notchian server only accepts digging packets with coordinates within a 6-unit radius between the center of the block and 1.5 units from the player's feet (not their eyes).
            var distance = (new Vector3(location.X + 0.5f, location.Y + 0.5f, location.Z + 0.5f)
                - new Vector3(playerPos.X, playerPos.Y + 1.5f, playerPos.Z)).Length();
            if (distance <= MaxDiggingRadius)
            {
                if (gameMode.ModeClass == GameMode.Class.Creative)
                {
                    var newState = BlockStates.Air();
                    await world.SetBlockState(GrainFactory, location, newState);
                }

                _diggingStartTick = await world.GetAge();
                _diggingBlock = (location, await world.GetBlockState(GrainFactory, location));
            }
        }

        public Task CancelDigging(BlockWorldPos location, PlayerDiggingFace face)
        {
            _diggingBlock = null;
            return Task.CompletedTask;
        }

        public async Task FinishDigging(BlockWorldPos location, PlayerDiggingFace face)
        {
            var gameMode = AttachedObject.GetValue(GameModeComponent.GameModeProperty);
            if (_diggingBlock != null)
            {
                var heldItem = await AttachedObject.Ask(new AskHeldItem());
                var itemHandler = ItemHandler.Create(new ItemState { Id = (uint)heldItem.slot.BlockId, MetaValue = 0 });

                var world = AttachedObject.GetWorld();
                var usedTick = (await world.GetAge()) - _diggingStartTick;
                if (await itemHandler.FinishedDigging(AttachedObject, GrainFactory, world, _diggingBlock.Value.Item1, _diggingBlock.Value.Item2, usedTick, gameMode))
                    return;
            }
        }
    }
}
