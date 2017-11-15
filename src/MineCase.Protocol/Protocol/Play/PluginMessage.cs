using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
#if !NET46
    [Orleans.Concurrency.Immutable]
#endif
    [Packet(0x0A)]
    public sealed class ServerboundPluginMessage
    {
        [SerializeAs(DataType.String)]
        public string Channel;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Data;

        public static ServerboundPluginMessage Deserialize(ref SpanReader br)
        {
            return new ServerboundPluginMessage
            {
                Channel = br.ReadAsString(),
                Data = br.ReadAsByteArray()
            };
        }
    }
}
