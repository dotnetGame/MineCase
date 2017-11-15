using System.IO;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x08)]
    public sealed class BlockBreakAnimation : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityID;

        [SerializeAs(DataType.Position)]
        public Position BlockPosition;

        [SerializeAs(DataType.Byte)]
        public byte DestoryStage;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(EntityID, out _);
            bw.WriteAsPosition(BlockPosition);
            bw.WriteAsByte(DestoryStage);
        }
    }
}
