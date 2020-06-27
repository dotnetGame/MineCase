using System;
using System.Collections.Generic;
using System.Text;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x09)]
    [GenerateSerializer]
    public sealed partial class ClickWindow : IPacket
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
    }
}
