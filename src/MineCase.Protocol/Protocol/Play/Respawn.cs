using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x3B)]
    [GenerateSerializer]
    public sealed partial class Respawn : IPacket
    {
        [SerializeAs(DataType.Int)]
        public int Dimension;

        [SerializeAs(DataType.Long)]
        public long HashedSeed;

        [SerializeAs(DataType.Byte)]
        public byte Gamemode;

        [SerializeAs(DataType.String)]
        public string LevelType;
    }
}
