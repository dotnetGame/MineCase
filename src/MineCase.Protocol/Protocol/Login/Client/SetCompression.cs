using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login.Client
{
    [Packet(0x03, ProtocolType.Login, PacketDirection.ClientBound)]
    public sealed class SetCompression : ISerializablePacket
    {
        [SerializeAs(DataType.VarInt)]
        public int Threshold;

        public void Deserialize(BinaryReader br)
        {
            Threshold = br.ReadAsVarInt(out _);
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsVarInt(Threshold, out _);
        }
    }
}
