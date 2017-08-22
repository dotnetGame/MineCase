using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    public sealed class Slot : ISerializablePacket
    {
        [SerializeAs(DataType.Short)]
        public short BlockId;

        [SerializeAs(DataType.Byte)]
        public byte? ItemCount;

        [SerializeAs(DataType.Short)]
        public short? ItemDamage;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsShort(BlockId);
            if (BlockId != -1)
            {
                bw.WriteAsByte(ItemCount.Value);
                bw.WriteAsShort(ItemDamage.Value);
                bw.WriteAsByte(0);
            }
        }
    }

    [Packet(0x14)]
    public sealed class WindowItems : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Count;

        [SerializeAs(DataType.Array)]
        public Slot[] Slots;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
            bw.WriteAsShort(Count);
            bw.WriteAsArray(Slots);
        }
    }
}
