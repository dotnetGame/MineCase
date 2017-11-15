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
    [Packet(0x00)]
    public sealed class TeleportConfirm
    {
        [SerializeAs(DataType.VarInt)]
        public uint TeleportId;

        public static TeleportConfirm Deserialize(ref SpanReader br)
        {
            return new TeleportConfirm
            {
                TeleportId = br.ReadAsVarInt(out _)
            };
        }
    }
}
