using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class InventorySlotArea : InventorySlotAreaBase
    {
        public InventorySlotArea(WindowGrain window, IGrainFactory grainFactory)
            : base(InventorySlotsCount, InventoryOffsetInContainer, window, grainFactory)
        {
        }
    }
}
