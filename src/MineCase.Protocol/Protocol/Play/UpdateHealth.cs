using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x49)]
    [GenerateSerializer]
    public sealed partial class UpdateHealth : IPacket
    {
        [SerializeAs(DataType.Float)]
        public float Health;

        [SerializeAs(DataType.VarInt)]
        public uint Food;

        [SerializeAs(DataType.Float)]
        public float FoodSaturation;
    }
}
