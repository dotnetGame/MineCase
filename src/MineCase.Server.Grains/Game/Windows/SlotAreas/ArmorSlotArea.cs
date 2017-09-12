using System;
using System.Collections.Generic;
using System.Text;
using Orleans;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class ArmorSlotArea : InventorySlotAreaBase
    {
        public ArmorSlotArea(WindowGrain window, IGrainFactory grainFactory)
            : base(ArmorSlotsCount, ArmorOffsetInContainer, window, grainFactory)
        {
        }
    }
}
