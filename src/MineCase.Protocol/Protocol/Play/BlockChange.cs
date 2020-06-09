using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0C)]
    [GenerateSerializer]
    public sealed partial class BlockChange : IPacket
    {
        [SerializeAs(DataType.Position)]
        public Position Location;

        [SerializeAs(DataType.VarInt)]
        public uint BlockId;
    }
}
