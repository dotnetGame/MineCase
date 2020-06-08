using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x01)]
    public sealed class PrepareCraftingGrid
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        public static PrepareCraftingGrid Deserialize(ref SpanReader br)
        {
            return new PrepareCraftingGrid
            {
                WindowId = br.ReadAsByte(),
                ActionNumber = br.ReadAsShort()
            };
        }
    }
}
