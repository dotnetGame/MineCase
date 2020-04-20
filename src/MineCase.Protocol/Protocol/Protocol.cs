using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Protocol
{
    public static class Protocol
    {
        public const uint Version = 578;

        public const string VersionName = "1.15.2";

        public static void ValidatePacketLength(uint length)
        {
            if (length > 16 * 1024)
                throw new ArgumentOutOfRangeException("Packet is too large.");
        }
    }
}
