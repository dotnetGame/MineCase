using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x15)]
    [GenerateSerializer]
    public sealed partial class WindowItems : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Count;

        [SerializeAs(DataType.SlotArray, ArrayLengthMember = nameof(Count))]
        public Slot[] Slots;
    }
}
