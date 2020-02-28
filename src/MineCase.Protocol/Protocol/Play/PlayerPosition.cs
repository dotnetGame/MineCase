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
    [Packet(0x11)]
    public sealed class PlayerPosition
    {
        [SerializeAs(DataType.Double)]
        public double X;

        [SerializeAs(DataType.Double)]
        public double FeetY;

        [SerializeAs(DataType.Double)]
        public double Z;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public static PlayerPosition Deserialize(ref SpanReader br)
        {
            return new PlayerPosition
            {
                X = br.ReadAsDouble(),
                FeetY = br.ReadAsDouble(),
                Z = br.ReadAsDouble(),
                OnGround = br.ReadAsBoolean()
            };
        }
    }
}
