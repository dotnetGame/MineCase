using MineCase.Formats;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x1D)]
    public sealed class ServerboundAnimation
    {
        [SerializeAs(DataType.VarInt)]
        public SwingHandState HandState;

        public static ServerboundAnimation Deserialize(ref SpanReader br)
        {
            return new ServerboundAnimation
            {
                // TODO: check VarInt to enum.
                HandState = (SwingHandState)br.ReadAsVarInt(out _)
            };
        }
    }
}
