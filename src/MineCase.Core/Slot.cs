using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Nbt;

namespace MineCase
{
    public struct Slot : IEquatable<Slot>
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

        public Slot CopyOne()
        {
            return new Slot { BlockId = BlockId, ItemCount = 1, ItemDamage = ItemDamage, NBT = NBT };
        }

        public override bool Equals(object obj)
        {
            return obj is Slot && Equals((Slot)obj);
        }

        public bool Equals(Slot other)
        {
            return IsEmpty == other.IsEmpty &&
                   BlockId == other.BlockId &&
                   ItemCount == other.ItemCount &&
                   ItemDamage == other.ItemDamage &&
                   EqualityComparer<NbtFile>.Default.Equals(NBT, other.NBT);
        }

        public override int GetHashCode()
        {
            var hashCode = -596621668;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + IsEmpty.GetHashCode();
            hashCode = hashCode * -1521134295 + BlockId.GetHashCode();
            hashCode = hashCode * -1521134295 + ItemCount.GetHashCode();
            hashCode = hashCode * -1521134295 + ItemDamage.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<NbtFile>.Default.GetHashCode(NBT);
            return hashCode;
        }

        public static bool operator ==(Slot slot1, Slot slot2)
        {
            return slot1.Equals(slot2);
        }

        public static bool operator !=(Slot slot1, Slot slot2)
        {
            return !(slot1 == slot2);
        }
    }
}
