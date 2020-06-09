using System.IO;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x09)]
    [GenerateSerializer]
    public sealed partial class BlockBreakAnimation : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityID;

        [SerializeAs(DataType.Position)]
        public Position BlockPosition;

        [SerializeAs(DataType.Byte)]
        public byte DestoryStage;
    }
}
