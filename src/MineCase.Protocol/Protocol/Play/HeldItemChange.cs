using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x1A)]
    public sealed class ServerboundHeldItemChange
    {
        [SerializeAs(DataType.Short)]
        public short Slot;

        public static ServerboundHeldItemChange Deserialize(ref SpanReader br)
        {
            return new ServerboundHeldItemChange
            {
                Slot = br.ReadAsShort()
            };
        }
    }
}
