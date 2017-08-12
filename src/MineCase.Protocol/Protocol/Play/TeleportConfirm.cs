using MineCase.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Play
{
    [Packet(0x00)]
    public sealed class TeleportConfirm
    {
        [SerializeAs(DataType.VarInt)]
        public uint TeleportId;

        public static TeleportConfirm Deserialize(BinaryReader br)
        {
            return new TeleportConfirm
            {
                TeleportId = br.ReadAsVarInt(out _)
            };
        }
    }
}
