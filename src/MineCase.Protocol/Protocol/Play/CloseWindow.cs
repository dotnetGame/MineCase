using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x0A)]
    public sealed class ServerboundCloseWindow
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        public static ServerboundCloseWindow Deserialize(ref SpanReader br)
        {
            return new ServerboundCloseWindow
            {
                WindowId = br.ReadAsByte()
            };
        }
    }

    [Packet(0x14)]
    public sealed class ClientboundCloseWindow : ISerializablePacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsByte(WindowId);
        }
    }
}
