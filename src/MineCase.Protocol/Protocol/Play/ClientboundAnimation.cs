using System.IO;

using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x06)]
    public sealed class ClientboundAnimation : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityID;

        [SerializeAs(DataType.Byte)]
        public ClientboundAnimationId AnimationID;

        public void Serialize(BinaryWriter bw)
        {
            // TODO: check enum to byte.
            byte animationID = (byte)AnimationID;
            bw.WriteAsVarInt(EntityID, out _);
            bw.WriteAsByte(animationID);
        }
    }
}
