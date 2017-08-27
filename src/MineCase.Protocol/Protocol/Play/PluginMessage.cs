using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;
using Orleans.Concurrency;

namespace MineCase.Protocol.Play
{
    [Immutable]
    [Packet(0x0A)]
    public sealed class ServerboundPluginMessage
    {
        [SerializeAs(DataType.String)]
        public string Channel;

        [SerializeAs(DataType.ByteArray)]
        public byte[] Data;

        public static ServerboundPluginMessage Deserialize(BinaryReader br)
        {
            return new ServerboundPluginMessage
            {
                Channel = br.ReadAsString(),
                Data = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position))
            };
        }
    }
}
