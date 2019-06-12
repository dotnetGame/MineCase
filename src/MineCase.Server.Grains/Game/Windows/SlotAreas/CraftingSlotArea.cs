using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using MineCase.Server.Game.Entities.Components;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class CraftingSlotArea : TemporarySlotArea
    {
        private readonly int _gridSize;
        private Slot[,] _afterSlots;

        public CraftingSlotArea(int gridSize, WindowGrain window, IGrainFactory grainFactory)
            : base(gridSize * gridSize + 1, window, grainFactory)
        {
            _gridSize = gridSize;
        }

        public override async Task Click(IPlayer player, List<SlotArea> slotAreas, int globalIndex, int slotIndex, ClickAction clickAction, Slot clickedItem)
        {
            if (slotIndex == 0)
            {
                var draggedSlot = await player.Ask(AskDraggedSlot.Default);
                var result = await GetSlot(player, 0);
                bool taken = false;
                switch (clickAction)
                {
                    case ClickAction.LeftMouseClick:
                    case ClickAction.RightMouseClick:
                        if (draggedSlot.IsEmpty)
                        {
                            await player.Tell(new SetDraggedSlot { Slot = result });
                            taken = true;
                        }
                        else if (draggedSlot.CanStack(result) && draggedSlot.ItemCount + result.ItemCount <= MaxStackCount)
                        {
                            draggedSlot.ItemCount += result.ItemCount;
                            await player.Tell(new SetDraggedSlot { Slot = draggedSlot });
                            taken = true;
                        }

                        break;
                    default:
                        break;
                }

                if (taken)
                {
                    await SetSlot(player, 0, Slot.Empty);
                    await SetCraftingGrid(player, _afterSlots);
                }
            }
            else
            {
                await base.Click(player, slotAreas, globalIndex, slotIndex, clickAction, clickedItem);
            }

            await UpdateRecipe(player);
        }

        private async Task UpdateRecipe(IPlayer player)
        {
            var grid = GetCraftingGrid(player);
            var recipe = await GrainFactory.GetGrain<ICraftingRecipes>(0).FindRecipe(grid.AsImmutable());
            if (recipe != null)
            {
                _afterSlots = recipe.AfterTake;
                await SetSlot(player, 0, recipe.Result);
            }
            else
            {
                _afterSlots = null;
                await SetSlot(player, 0, Slot.Empty);
            }
        }

        private Slot[,] GetCraftingGrid(IPlayer player)
        {
            var grid = new Slot[_gridSize, _gridSize];

            int x = 0, y = 0;
            foreach (var slot in GetSlots(player).Skip(1))
            {
                grid[x++, y] = slot;
                if (x == _gridSize)
                {
                    x = 0;
                    y++;
                }
            }

            return grid;
        }

        private async Task SetCraftingGrid(IPlayer player, Slot[,] afterSlots)
        {
            int index = 1;
            for (int y = 0; y < afterSlots.GetUpperBound(1) + 1; y++)
            {
                for (int x = 0; x < afterSlots.GetUpperBound(0) + 1; x++)
                {
                    await SetSlot(player, index++, afterSlots[x, y]);
                }
            }
        }
    }
}
