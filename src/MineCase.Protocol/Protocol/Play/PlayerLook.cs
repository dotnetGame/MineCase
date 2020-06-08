using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // FIXME : 1.15.2 does not have this packet
    [Packet(0x10)]
    public sealed class PlayerLook
    {
        [SerializeAs(DataType.Float)]
        public float Yaw;

        [SerializeAs(DataType.Float)]
        public float Pitch;

        [SerializeAs(DataType.Boolean)]
        public bool OnGround;

        public static PlayerLook Deserialize(ref SpanReader br)
        {
            return new PlayerLook
            {
                Yaw = br.ReadAsFloat(),
                Pitch = br.ReadAsFloat(),
                OnGround = br.ReadAsBoolean()
            };
        }
    }
}
