using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Formats
{
    public sealed class Slot
    {
        public static Slot Empty { get; } = new Slot();

        public bool IsEmpty => BlockId == -1;

        public short BlockId { get; set; } = -1;

        public byte ItemCount { get; set; }

        public short ItemDamage { get; set; }
    }
}
