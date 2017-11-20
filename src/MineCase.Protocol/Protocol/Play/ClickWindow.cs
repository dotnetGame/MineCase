using System;
using System.Collections.Generic;
using System.Text;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x08)]
    public sealed class ClickWindow
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Slot;

        [SerializeAs(DataType.Byte)]
        public byte Button;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.VarInt)]
        public uint Mode;

        [SerializeAs(DataType.Slot)]
        public Slot ClickedItem;

        public static ClickWindow Deserialize(ref SpanReader br)
        {
            return new ClickWindow
            {
                WindowId = br.ReadAsByte(),
                Slot = br.ReadAsShort(),
                Button = br.ReadAsByte(),
                ActionNumber = br.ReadAsShort(),
                Mode = br.ReadAsVarInt(out _),
                ClickedItem = br.ReadAsSlot()
            };
        }
    }
}
