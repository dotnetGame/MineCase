using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x3F)]
    public sealed class SetExperience : ISerializablePacket
    {
        [SerializeAs(DataType.Float)]
        public float ExperienceBar;

        [SerializeAs(DataType.VarInt)]
        public uint Level;

        [SerializeAs(DataType.VarInt)]
        public uint TotalExperience;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsFloat(ExperienceBar);
            bw.WriteAsVarInt(Level, out _);
            bw.WriteAsVarInt(TotalExperience, out _);
        }
    }
}
