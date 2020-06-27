using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Command;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x12)]
    public sealed class DeclareCommands : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Count;

        [SerializeAs(DataType.Array)]
        public Node[] Nodes;

        [SerializeAs(DataType.VarInt)]
        public uint RootIndex;

        // TODO : complete serialization and deserialization
        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(Count, out _);
        }

        public void Deserialize(ref SpanReader br)
        {
            Count = br.ReadAsVarInt(out _);
        }
    }
}
