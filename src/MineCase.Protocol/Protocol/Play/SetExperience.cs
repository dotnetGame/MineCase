using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x48)]
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
