using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x56)]
    [GenerateSerializer]
    public sealed partial class CollectItem : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint CollectedEntityId;

        [SerializeAs(DataType.VarInt)]
        public uint CollectorEntityId;

        [SerializeAs(DataType.VarInt)]
        public uint PickupItemCount;
    }
}
