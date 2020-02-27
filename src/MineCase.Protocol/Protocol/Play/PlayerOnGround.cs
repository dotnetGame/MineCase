using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // FIXME : 1.15.2 does not have this packet
    [Packet(0x0D)]
    public sealed class PlayerOnGround
    {
        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public static PlayerOnGround Deserialize(ref SpanReader br)
        {
            return new PlayerOnGround
            {
                OnGround = br.ReadAsBoolean(),
            };
        }
    }
}
