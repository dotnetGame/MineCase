using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x4F)]
    public sealed class TimeUpdate : ISerializablePacket
    {
        [SerializeAs(DataType.Long)]
        public long WorldAge;

        [SerializeAs(DataType.Long)]
        public long TimeOfDay;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsLong(WorldAge);
            bw.WriteAsLong(TimeOfDay);
        }
    }
}
