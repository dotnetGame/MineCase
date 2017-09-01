using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    public enum Hand : uint
    {
        Main = 0,
        Off = 1
    }

    [Immutable]
    [Packet(0x1D)]
    public sealed class ServerboundAnimation
    {
        [SerializeAs(DataType.VarInt)]
        public Hand Hand;

        public static ServerboundAnimation Deserialize(ref SpanReader br)
        {
            return new ServerboundAnimation
            {
                Hand = (Hand)br.ReadAsVarInt(out _)
            };
        }
    }
}
