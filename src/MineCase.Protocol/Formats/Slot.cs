using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Formats
{
    public struct Slot
    {
        public static Slot Empty { get; } = new Slot { BlockId = -1 };

        public bool IsEmpty => BlockId == -1 || ItemCount == 0;

        public short BlockId { get; set; }

        public byte ItemCount { get; set; }

        public short ItemDamage { get; set; }

        public bool CanStack(Slot slot)
        {
            return slot.BlockId == BlockId && ItemDamage == slot.ItemDamage;
        }
    }
}
