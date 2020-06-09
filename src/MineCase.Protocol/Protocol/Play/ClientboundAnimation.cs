using System.IO;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x06)]
    [GenerateSerializer]
    public sealed partial class ClientboundAnimation : IPacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityID;

        [SerializeAs(DataType.Byte)]
        public ClientboundAnimationId AnimationID;
    }
}
