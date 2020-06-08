using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

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

        public static ServerboundConfirmTransaction Deserialize(ref SpanReader br)
        {
            return new ServerboundConfirmTransaction
            {
                WindowId = br.ReadAsByte(),
                ActionNumber = br.ReadAsShort(),
                Accepted = br.ReadAsBoolean()
            };
        }
    }

    [Packet(0x11)]
    public sealed class ClientboundConfirmTransaction : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.Boolean)]
        public bool Accepted;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
            bw.WriteAsShort(ActionNumber);
            bw.WriteAsBoolean(Accepted);
        }
    }
}
