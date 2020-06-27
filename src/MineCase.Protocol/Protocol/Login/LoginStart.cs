using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Serialization;

namespace MineCase.Protocol.Login
{
    [Packet(0x00)]
    [GenerateSerializer]
    public sealed partial class LoginStart : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Name;
    }

    [Packet(0x00)]
    [GenerateSerializer]
    public sealed partial class LoginDisconnect : IPacket
    {
        [SerializeAs(DataType.String)]
        public string Reason;
    }

    [Packet(0x02)]
    [GenerateSerializer]
    public sealed partial class LoginSuccess : IPacket
    {
        [SerializeAs(DataType.String)]
        public string UUID;

        [SerializeAs(DataType.String)]
        public string Username;
    }
}
