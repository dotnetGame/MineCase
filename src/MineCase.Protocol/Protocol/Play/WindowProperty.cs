using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x15)]
    public sealed class WindowProperty : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short Property;

        [SerializeAs(DataType.Short)]
        public short Value;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte((sbyte)WindowId);
            bw.WriteAsShort(Property);
            bw.WriteAsShort(Value);
        }
    }
}
