using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    // In 1.12, it is PlayerListItem. Now it is PlayerInfo
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x34)]
    public sealed class PlayerInfo<TAction> : ISerializablePacket
        where TAction : PlayerInfoAction
    {
        [SerializeAs(DataType.VarInt)]
        public uint Action;

        [SerializeAs(DataType.VarInt)]
        public uint NumberOfPlayers;

        [SerializeAs(DataType.Array)]
        public TAction[] Players;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(Action, out _);
            bw.WriteAsVarInt(NumberOfPlayers, out _);
            if (NumberOfPlayers != 0)
                bw.WriteAsArray(Players);
        }
    }

    public abstract class PlayerInfoAction : ISerializablePacket
    {
        [SerializeAs(DataType.UUID)]
        public Guid UUID;

        public virtual void Serialize(BinaryWriter bw)
        {
            bw.WriteAsUUID(UUID);
        }
    }

    public sealed class PlayerInfoAddPlayerAction : PlayerInfoAction
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
