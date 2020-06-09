using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x48)]
    [GenerateSerializer]
    public sealed partial class SetExperience : IPacket
    {
        [SerializeAs(DataType.Float)]
        public float ExperienceBar;

        [SerializeAs(DataType.VarInt)]
        public uint Level;

        [SerializeAs(DataType.VarInt)]
        public uint TotalExperience;
    }
}
