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
    [Packet(0x23)]
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
