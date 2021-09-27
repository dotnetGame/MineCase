using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Nbt.Tags;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x26)]
    [GenerateSerializer]
    public sealed partial class JoinGame : IPacket
    {
        [SerializeAs(DataType.Int)]
        public int EID;

        [SerializeAs(DataType.Boolean)]
        public bool IsHardcore;

        [SerializeAs(DataType.Byte)]
        public byte GameMode;

        [SerializeAs(DataType.Byte)]
        public byte PreviousGameMode;

        [SerializeAs(DataType.VarInt)]
        public uint WorldCount;

        [SerializeAs(DataType.IdentifierArray, ArrayLengthMember = nameof(WorldCount))]
        public string[] WorldNames;

        [SerializeAs(DataType.NBTTag)]
        public NbtCompound DimensionCodec;

        [SerializeAs(DataType.NBTTag)]
        public NbtCompound Dimension;

        [SerializeAs(DataType.Identifier)]
        public string WorldName;

        [SerializeAs(DataType.Long)]
        public long HashedSeed;

        [SerializeAs(DataType.VarInt)]
        public uint MaxPlayers;

        [SerializeAs(DataType.VarInt)]
        public uint ViewDistance;

        [SerializeAs(DataType.Boolean)]
        public bool ReducedDebugInfo;

        [SerializeAs(DataType.Boolean)]
        public bool EnableRespawnScreen;

        [SerializeAs(DataType.Boolean)]
        public bool IsDebug;

        [SerializeAs(DataType.Boolean)]
        public bool IsFlat;
    }
}
