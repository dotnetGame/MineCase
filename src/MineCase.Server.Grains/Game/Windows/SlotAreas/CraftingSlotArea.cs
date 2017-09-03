using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Entities;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class CraftingSlotArea : TemporarySlotArea
    {
        private readonly int _gridSize;

        public CraftingSlotArea(int gridSize, WindowGrain window, IGrainFactory grainFactory)
            : base(gridSize * gridSize + 1, window, grainFactory)
        {
            _gridSize = gridSize;
        }

        public override async Task Click(IPlayer player, int slotIndex, ClickAction clickAction, Slot clickedItem)
        {
            await base.Click(player, slotIndex, clickAction, clickedItem);
            await UpdateRecipe(player);
        }

        private Task UpdateRecipe(IPlayer player)
        {
            var grid = GetCraftingGrid(player);
            return Task.CompletedTask;
        }

        private Slot[] GetCraftingGrid(IPlayer player)
        {
            return GetSlots(player).Skip(1).ToArray();
        }
    }
}
