using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x00)]
    public sealed class LoginStart : ISerializablePacket
    {
        [SerializeAs(DataType.String)]
        public string Name;

        public static LoginStart Deserialize(ref SpanReader br)
        {
            return new LoginStart
            {
                Name = br.ReadAsString(),
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(Name);
        }
    }

    [Packet(0x00)]
    public sealed class LoginDisconnect : ISerializablePacket
    {
        [SerializeAs(DataType.Chat)]
        public string Reason;

        public static LoginDisconnect Deserialize(ref SpanReader br)
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

        public static LoginSuccess Deserialize(ref SpanReader br)
        {
            return new LoginSuccess
            {
                UUID = br.ReadAsString(),
                Username = br.ReadAsString()
            };
        }

        public void Serialize(BinaryWriter bw)
        {
            bw.WriteAsString(UUID);
            bw.WriteAsString(Username);
        }
    }
}
