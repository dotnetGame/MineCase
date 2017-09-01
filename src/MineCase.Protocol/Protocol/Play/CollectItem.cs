﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x4A)]
    public sealed class CollectItem : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint CollectedEntityId;

        [SerializeAs(DataType.VarInt)]
        public uint CollectorEntityId;

        [SerializeAs(DataType.VarInt)]
        public uint PickupItemCount;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(CollectedEntityId, out _);
            bw.WriteAsVarInt(CollectorEntityId, out _);
            bw.WriteAsVarInt(PickupItemCount, out _);
        }
    }
}
