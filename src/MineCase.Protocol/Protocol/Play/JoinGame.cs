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
    [Packet(0x26)]
    public class JoinGame : ISerializablePacket
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

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(EID);
            bw.WriteAsByte(GameMode);
            bw.WriteAsInt(Dimension);
            bw.WriteAsLong(HashedSeed);
            bw.WriteAsByte(MaxPlayers);
            bw.WriteAsString(LevelType);
            bw.WriteAsVarInt(ViewDistance, out _);
            bw.WriteAsBoolean(ReducedDebugInfo);
            bw.WriteAsBoolean(EnableRespawnScreen);
        }
    }
}
