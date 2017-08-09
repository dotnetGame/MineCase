using MineCase.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MineCase.Protocol.Play
{
    [Packet(0x06)]
    public sealed class ServerboundConfirmTransaction
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.Boolean)]
        public bool Accepted;

        public static ServerboundConfirmTransaction Deserialize(BinaryReader br)
        {
            return new ServerboundConfirmTransaction
            {
                WindowId = br.ReadAsByte(),
                ActionNumber = br.ReadAsShort(),
                Accepted = br.ReadAsBoolean()
            };
        }
    }
}
