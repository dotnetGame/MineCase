using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x38)]
    [GenerateSerializer]
    public sealed partial class DestroyEntities : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint Count;

        [SerializeAs(DataType.VarIntArray, ArrayLengthMember = nameof(Count))]
        public uint[] EntityIds;
    }
}
