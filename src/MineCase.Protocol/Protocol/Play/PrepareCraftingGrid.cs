using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x01)]
    public sealed class PrepareCraftingGrid
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowID;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        public static PrepareCraftingGrid Deserialize(BinaryReader br)
        {
            throw new NotImplementedException();
        }
    }
}
