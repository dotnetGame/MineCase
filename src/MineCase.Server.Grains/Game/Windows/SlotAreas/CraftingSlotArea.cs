using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Formats;
using MineCase.Server.Game.Entities;
using Orleans.Concurrency;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class CraftingSlotArea : TemporarySlotArea
    {
        private readonly int _gridSize;

        public CraftingSlotArea(int gridSize, WindowGrain window)
            : base(gridSize * gridSize + 1, window)
        {
            _gridSize = gridSize;
        }
    }
}
