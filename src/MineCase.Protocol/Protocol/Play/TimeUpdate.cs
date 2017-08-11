using MineCase.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Play
{
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
