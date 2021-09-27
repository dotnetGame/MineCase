using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Protocol
{
    public static class Protocol
    {
        public const uint Version = 756;

        public const string VersionName = "1.17.1";

        public const uint SetCompressionPacketId = 0x03;

        public static void ValidatePacketLength(uint length)
        {
            if (length > 16 * 1024)
                throw new ArgumentOutOfRangeException("Packet is too large.");
        }
    }
}
