using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Protocol.Protocol
{
    public enum PacketDirection
    {
        /// <summary>
        /// Packet server bound
        /// </summary>
        ServerBound = 0,

        /// <summary>
        /// Packet client bound
        /// </summary>
        ClientBound = 1,
    }
}
