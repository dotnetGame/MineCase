using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x05)]
    [GenerateSerializer]
    public sealed partial class ClientSettings : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Locale;

        [SerializeAs(DataType.Byte)]
        public byte ViewDistance;

        [SerializeAs(DataType.VarInt)]
        public uint ChatMode;

        [SerializeAs(DataType.Boolean)]
        public bool ChatColors;

        [SerializeAs(DataType.Byte)]
        public byte DisplayedSkinParts;

        [SerializeAs(DataType.VarInt)]
        public uint MainHand;
    }
}
