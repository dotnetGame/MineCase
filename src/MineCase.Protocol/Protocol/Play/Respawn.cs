using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Play
{
    [Packet(0x3B)]
    public sealed class Respawn : ISerializablePacket
    {
        [SerializeAs(DataType.Int)]
        public int Dimension;

        [SerializeAs(DataType.Long)]
        public long HashedSeed;

        [SerializeAs(DataType.Byte)]
        public byte Gamemode;

        [SerializeAs(DataType.String)]
        public string LevelType;

        public static Respawn Deserialize(ref SpanReader br)
        {
            return new Respawn
            {
                Dimension = br.ReadAsInt(),
                HashedSeed = br.ReadAsLong(),
                Gamemode = br.ReadAsByte(),
                LevelType = br.ReadAsString()
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsInt(Dimension);
            bw.WriteAsLong(HashedSeed);
            bw.WriteAsByte(Gamemode);
            bw.WriteAsString(LevelType);
        }
    }
}
