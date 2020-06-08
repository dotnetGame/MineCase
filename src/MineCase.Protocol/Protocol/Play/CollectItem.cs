using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x56)]
    public sealed class CollectItem : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint CollectedEntityId;

        [SerializeAs(DataType.VarInt)]
        public uint CollectorEntityId;

        [SerializeAs(DataType.VarInt)]
        public uint PickupItemCount;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(CollectedEntityId, out _);
            bw.WriteAsVarInt(CollectorEntityId, out _);
            bw.WriteAsVarInt(PickupItemCount, out _);
        }
    }
}
