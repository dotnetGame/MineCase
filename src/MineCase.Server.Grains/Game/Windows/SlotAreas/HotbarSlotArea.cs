using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class HotbarSlotArea : InventorySlotAreaBase
    {
        public HotbarSlotArea(WindowGrain window, IGrainFactory grainFactory)
            : base(HotbarSlotsCount, HotbarOffsetInContainer, window, grainFactory)
        {
        }
    }
}
