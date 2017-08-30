using System.IO;
using MineCase.Formats;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x06)]
    public sealed class ClientAnimation : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public uint EntityID;

        [SerializeAs(DataType.Byte)]
        public ClientAnimationID AnimationID;

        public void Serialize(BinaryWriter bw)
        {
            // TODO: check enum to byte.
            byte animationID = (byte)AnimationID;
            bw.WriteAsVarInt(EntityID, out _);
            bw.WriteAsByte(animationID);
        }
    }
}
