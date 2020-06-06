using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Command;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x12)]
    public sealed class DeclareCommands : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Count;

        [SerializeAs(DataType.Array)]
        public CommandNode[] Nodes;

        [SerializeAs(DataType.VarInt)]
        public uint RootIndex;

        // TODO : complete serialization and deserialization
        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(Count, out _);
        }

        public static DeclareCommands Deserialize(ref SpanReader br)
        {
            return new DeclareCommands
            {
                Count = br.ReadAsVarInt(out _),
            };
        }
    }
}
