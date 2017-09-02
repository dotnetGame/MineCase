using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x20)]
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
