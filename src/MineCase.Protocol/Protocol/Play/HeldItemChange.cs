using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x23)]
    [GenerateSerializer]
    public sealed partial class ServerboundHeldItemChange : IPacket
    {
        [SerializeAs(DataType.Short)]
        public short Slot;
    }
}
