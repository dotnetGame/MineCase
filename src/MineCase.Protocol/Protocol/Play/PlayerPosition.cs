using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x0E)]
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

        public static PlayerPosition Deserialize(BinaryReader br)
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
