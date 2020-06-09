using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x44)]
    public sealed class EntityMetadata : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityId;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Metadata;

        public void Deserialize(ref SpanReader br)
        {
            throw new NotImplementedException();
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EntityId, out _);
            bw.WriteAsByteArray(Metadata);
        }
    }

    public enum EntityMetadataType : byte
    {
        Byte = 0,
        VarInt = 1,
        Float = 2,
        String = 3,
        Chat = 4,
        Slot = 5,
        Boolean = 6,
        Rotation = 7,
        Position = 8,
        OptPosition = 9,
        Direction = 10,
        OptUUID = 11,
        OptBlockID = 12,
        NBTTag = 13
    }
}
