using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x09)]
    public sealed class CloseWindow
    {
        [SerializeAs(DataType.Byte)]
        public byte WindowId;

        public static CloseWindow Deserialize(ref SpanReader br)
        {
            return new CloseWindow
            {
                WindowId = br.ReadAsByte()
            };
        }
    }
}
