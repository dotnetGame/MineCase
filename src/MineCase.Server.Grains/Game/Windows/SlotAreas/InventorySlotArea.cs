using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class InventorySlotArea : InventorySlotAreaBase
    {
        public InventorySlotArea(WindowGrain window)
            : base(InventorySlotsCount, InventoryOffsetInContainer, window)
        {
        }
    }
}
