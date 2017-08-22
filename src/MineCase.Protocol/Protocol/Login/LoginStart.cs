using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x00)]
    public sealed class LoginStart
    {
        [SerializeAs(DataType.String)]
        public string Name;

        public static LoginStart Deserialize(BinaryReader br)
        {
            return new LoginStart
            {
                Name = br.ReadAsString(),
            };
        }
    }

    [Packet(0x00)]
    public sealed class LoginDisconnect : ISerializablePacket
    {
        [SerializeAs(DataType.Chat)]
        public string Reason;

        public static LoginDisconnect Deserialize(BinaryReader br)
        {
            return new LoginDisconnect
            {
                Reason = br.ReadAsString()
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Reason);
        }
    }

    [Packet(0x00)]
    public sealed class LoginDisconnect : ISerializablePacket
    {
        [SerializeAs(DataType.Chat)]
        public string Reason;

        public static LoginDisconnect Deserialize(BinaryReader br)
        {
            return new LoginDisconnect
            {
                Reason = br.ReadAsString()
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Reason);
        }
    }

    [Packet(0x02)]
    public sealed class LoginSuccess : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string UUID;

        [SerializeAs(DataType.String)]
        public string Username;

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(UUID);
            bw.WriteAsString(Username);
        }
    }
}
