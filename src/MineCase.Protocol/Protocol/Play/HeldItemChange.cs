using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x1A)]
    public sealed class ServerboundHeldItemChange
    {
        [SerializeAs(DataType.Short)]
        public short Slot;

        public static ServerboundHeldItemChange Deserialize(BinaryReader br)
        {
            return new ServerboundHeldItemChange
            {
                Slot = br.ReadAsShort()
            };
        }
    }
}
