using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x4F)]
    [GenerateSerializer]
    public sealed partial class TimeUpdate : IPacket
    {
        [SerializeAs(DataType.Long)]
        public long WorldAge;

        [SerializeAs(DataType.Long)]
        public long TimeOfDay;
    }
}
