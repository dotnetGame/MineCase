using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class OffhandSlotArea : InventorySlotAreaBase
    {
        public OffhandSlotArea(WindowGrain window)
            : base(OffhandSlotsCount, OffhandOffsetInContainer, window)
        {
        }
    }
}
