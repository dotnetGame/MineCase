using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x26)]
    [GenerateSerializer]
    public sealed partial class JoinGame : IPacket
    {
        [SerializeAs(DataType.Int)]
        public int EID;

        [SerializeAs(DataType.Byte)]
        public byte GameMode;

        [SerializeAs(DataType.Int)]
        public int Dimension;

        [SerializeAs(DataType.Long)]
        public long HashedSeed;

        [SerializeAs(DataType.Byte)]
        public byte MaxPlayers;

        [SerializeAs(DataType.String)]
        public string LevelType;

        [SerializeAs(DataType.VarInt)]
        public uint ViewDistance;

        [SerializeAs(DataType.Boolean)]
        public bool ReducedDebugInfo;

        [SerializeAs(DataType.Boolean)]
        public bool EnableRespawnScreen;
    }
}
