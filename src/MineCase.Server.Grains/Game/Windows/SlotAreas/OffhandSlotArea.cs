using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class OffhandSlotArea : InventorySlotAreaBase
    {
        public OffhandSlotArea(WindowGrain window, IGrainFactory grainFactory)
            : base(OffhandSlotsCount, OffhandOffsetInContainer, window, grainFactory)
        {
        }
    }
}
