using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x2D)]
    [GenerateSerializer]
    public sealed partial class UseItem : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public Hand Hand;
    }
}
