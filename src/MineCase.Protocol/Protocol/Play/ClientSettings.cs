using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x05)]
    public sealed class ClientSettings
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

        public static ClientSettings Deserialize(ref SpanReader br)
        {
            return new ClientSettings
            {
                Locale = br.ReadAsString(),
                ViewDistance = br.ReadAsByte(),
                ChatMode = br.ReadAsVarInt(out _),
                ChatColors = br.ReadAsBoolean(),
                DisplayedSkinParts = br.ReadAsByte(),
                MainHand = br.ReadAsVarInt(out _)
            };
        }
    }
}
