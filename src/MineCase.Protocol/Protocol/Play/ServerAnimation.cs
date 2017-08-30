using MineCase.Formats;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x1D)]
    public sealed class ServerAnimation
    {
        [SerializeAs(DataType.VarInt)]
        public SwingHandState HandState;

        public static ServerAnimation Deserialize(ref SpanReader br)
        {
            return new ServerAnimation
            {
                // TODO: check VarInt to enum.
                HandState = (SwingHandState)br.ReadAsVarInt(out _)
            };
        }
    }
}
