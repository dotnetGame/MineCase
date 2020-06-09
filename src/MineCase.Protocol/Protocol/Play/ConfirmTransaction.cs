using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x06)]
    [GenerateSerializer]
    public sealed partial class ServerboundConfirmTransaction : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.Boolean)]
        public bool Accepted;
    }

    [Packet(0x11)]
    [GenerateSerializer]
    public sealed partial class ClientboundConfirmTransaction : IPacket
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        [SerializeAs(DataType.Short)]
        public short ActionNumber;

        [SerializeAs(DataType.Boolean)]
        public bool Accepted;
    }
}
