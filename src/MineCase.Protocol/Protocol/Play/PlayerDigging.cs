using MineCase.Formats;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x14)]
    public sealed class PlayerDigging
    {
        [SerializeAs(DataType.VarInt)]
        public DiggingStatus Status;

        [SerializeAs(DataType.Position)]
        public Position TargetPosition;

        [SerializeAs(DataType.Byte)]
        public DiggingFace Face;

        public static PlayerDigging Deserialize(ref SpanReader br)
        {
            return new PlayerDigging
            {
                // TODO:check integral to enum.
                Status = (DiggingStatus)br.ReadAsVarInt(out _),
                TargetPosition = br.ReadAsPosition(),
                Face = (DiggingFace)br.ReadAsByte()
            };
        }
    }
}
