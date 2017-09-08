using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x09)]
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

    [Immutable]
    [Packet(0x12)]
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
