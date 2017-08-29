using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
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
