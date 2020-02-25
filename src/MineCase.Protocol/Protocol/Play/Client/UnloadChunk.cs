using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play.Client
{
    [Packet(0x1E, ProtocolType.Play, PacketDirection.ClientBound)]
    public sealed class UnloadChunk : ISerializablePacket
    {
        [SerializeAs(DataType.Int)]
        public int ChunkX;

        [SerializeAs(DataType.Int)]
        public int ChunkZ;

        public void Deserialize(BinaryReader br)
        {
            ChunkX = br.ReadAsInt();
            ChunkZ = br.ReadAsInt();
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(ChunkX);
            bw.WriteAsInt(ChunkZ);
        }
    }
}
