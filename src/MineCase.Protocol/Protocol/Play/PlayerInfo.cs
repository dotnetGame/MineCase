using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // In 1.12, it is PlayerListItem. Now it is PlayerInfo
    [Packet(0x34)]
    [GenerateSerializer]
    public sealed partial class PlayerInfo<TAction> : IPacket
        where TAction : PlayerInfoAction, new()
    {
        [SerializeAs(DataType.VarInt)]
        public uint Action;

        [SerializeAs(DataType.VarInt)]
        public uint NumberOfPlayers;

        [SerializeAs(DataType.Array, ArrayLengthMember = nameof(NumberOfPlayers))]
        public TAction[] Players;
    }

    public abstract partial class PlayerInfoAction : IPacket
    {
        [SerializeAs(DataType.UUID)]
        public Guid UUID;

        public virtual void Deserialize(ref SpanReader br)
        {
            UUID = br.ReadAsUUID();
        }

        public virtual void Serialize(BinaryWriter bw)
        {
            bw.WriteAsUUID(UUID);
        }
    }

    public sealed partial class PlayerInfoAddPlayerAction : PlayerInfoAction
    {
        [SerializeAs(DataType.String)]
        public string Name;

        [SerializeAs(DataType.VarInt)]
        public uint NumberOfProperties;

        [SerializeAs(DataType.VarInt)]
        public uint GameMode;

        [SerializeAs(DataType.VarInt)]
        public uint Ping;

        [SerializeAs(DataType.Boolean)]
        public bool HasDisplayName;

        [SerializeAs(DataType.Chat)]
        public string DisplayName;

        public override void Deserialize(ref SpanReader br)
        {
            base.Deserialize(ref br);
        }

        public override void Serialize(BinaryWriter bw)
        {
            base.Serialize(bw);

            bw.WriteAsString(Name);
            bw.WriteAsVarInt(NumberOfProperties, out _);
            bw.WriteAsVarInt(GameMode, out _);
            bw.WriteAsVarInt(Ping, out _);
            bw.WriteAsBoolean(HasDisplayName);
            if (HasDisplayName)
                throw new NotImplementedException();
        }
    }

    public sealed class PlayerInfoRemovePlayerAction : PlayerInfoAction
    {
    }
}
