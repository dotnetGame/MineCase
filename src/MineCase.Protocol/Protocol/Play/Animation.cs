using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    public enum Hand : uint
    {
        Main = 0,
        Off = 1
    }

    [Packet(0x2A)]
    [GenerateSerializer]
    public sealed partial class ServerboundAnimation : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public Hand Hand;
    }
}
