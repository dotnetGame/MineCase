using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Game.Windows.SlotAreas
{
    internal class ArmorSlotArea : InventorySlotAreaBase
    {
        public ArmorSlotArea(WindowGrain window)
            : base(ArmorSlotsCount, ArmorOffsetInContainer, window)
        {
        }
    }
}
