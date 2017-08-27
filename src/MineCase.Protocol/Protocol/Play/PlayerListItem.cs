﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x2D)]
    public sealed class PlayerListItem<TAction> : ISerializablePacket
        where TAction : PlayerListItemAction
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

    public abstract class PlayerListItemAction : ISerializablePacket
    {
        [SerializeAs(DataType.UUID)]
        public Guid UUID;

        public virtual void Serialize(BinaryWriter bw)
        {
            bw.WriteAsUUID(UUID);
        }
    }

    public sealed class PlayerListItemAddPlayerAction : PlayerListItemAction
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
}
