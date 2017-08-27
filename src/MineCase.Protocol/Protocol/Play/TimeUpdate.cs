using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x46)]
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
