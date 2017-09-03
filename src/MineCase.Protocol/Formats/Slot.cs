using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Nbt;

namespace MineCase.Formats
{
    public struct Slot
    {
        public static Slot Empty { get; } = new Slot { BlockId = -1 };

        public bool IsEmpty => BlockId == -1;

        public short BlockId { get; set; }

        public byte ItemCount { get; set; }

        public short ItemDamage { get; set; }

        public NbtFile NBT { get; set; }

        public bool CanStack(Slot slot)
        {
            return slot.BlockId == BlockId && ItemDamage == slot.ItemDamage;
        }

        public Slot WithItemCount(byte count)
        {
            return new Slot
            {
                BlockId = BlockId,
                ItemCount = count,
                ItemDamage = ItemDamage,
                NBT = NBT
            };
        }

        public void MakeEmptyIfZero()
        {
            if (ItemCount == 0)
                this = Empty;
        }
    }
}
