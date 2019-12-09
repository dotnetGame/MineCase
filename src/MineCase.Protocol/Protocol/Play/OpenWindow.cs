using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x13)]
    public sealed class OpenWindow : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.String)]
        public string WindowType;

        [SerializeAs(DataType.Chat)]
        public Chat WindowTitle;

        [SerializeAs(DataType.Byte)]
        public byte NumberOfSlots;

        [SerializeAs(DataType.Byte)]
        public byte? EntityId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte((sbyte)WindowId);
            bw.WriteAsString(WindowType);
            bw.WriteAsChat(WindowTitle);
            bw.WriteAsByte((sbyte)NumberOfSlots);
            if (EntityId.HasValue)
                bw.WriteAsByte((sbyte)EntityId.Value);
        }
    }
}
