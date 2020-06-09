using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // FIXME : 1.15.2 does not have this packet
    [Packet(0x0D)]
    [GenerateSerializer]
    public sealed partial class PlayerOnGround : IPacket
    {
        [SerializeAs(DataType.Boolean)]
        public bool OnGround;
    }
}
