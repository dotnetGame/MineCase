﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x14)]
    public sealed class WindowItems : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Count;

        [SerializeAs(DataType.Array)]
        public Slot[] Slots;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
            bw.WriteAsShort(Count);
            foreach (var slot in Slots)
                bw.WriteAsSlot(slot);
        }
    }
}
