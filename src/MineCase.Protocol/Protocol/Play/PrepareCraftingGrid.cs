using MineCase.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Play
{
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
