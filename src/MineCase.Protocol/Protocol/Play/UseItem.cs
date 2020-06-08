using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x2D)]
    public sealed class UseItem
    {
        [SerializeAs(DataType.VarInt)]
        public Hand Hand;

        public static UseItem Deserialize(ref SpanReader br)
        {
            return new UseItem
            {
                Hand = (Hand)br.ReadAsVarInt(out _)
            };
        }
    }
}
