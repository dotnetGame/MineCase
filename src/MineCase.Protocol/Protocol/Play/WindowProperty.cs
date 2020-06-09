using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x15)]
    [GenerateSerializer]
    public sealed partial class WindowProperty : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Property;

        [SerializeAs(DataType.Short)]
        public short Value;
    }
}
