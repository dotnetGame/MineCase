using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x41)]
    [GenerateSerializer]
    public sealed partial class UpdateViewPosition : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public int ChunkX;

        [SerializeAs(DataType.VarInt)]
        public int ChunkZ;
    }
}
