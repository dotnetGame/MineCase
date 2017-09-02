using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class HotbarSlotArea : InventorySlotAreaBase
    {
        public HotbarSlotArea(WindowGrain window)
            : base(HotbarSlotsCount, HotbarOffsetInContainer, window)
        {
        }
    }
}
