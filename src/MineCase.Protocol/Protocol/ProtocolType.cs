using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Protocol.Status;

namespace MineCase.Protocol.Protocol
{
    public enum ProtocolType
    {
        /// <summary>
        /// Handshake packet
        /// </summary>
        Handshake = -1,

        /// <summary>
        /// Play packet
        /// </summary>
        Play = 0,

        /// <summary>
        /// Status packet
        /// </summary>
        Status = 1,

        /// <summary>
        /// Login packet
        /// </summary>
        Login = 2,
    }

    public class PacketInfo
    {
        private Dictionary<Type, int> _typeIdPairs;

        public PacketInfo()
        {
            _typeIdPairs.Add(typeof(Ping), 0x01);
        }

        public int GetPacketId(ISerializablePacket packet)
        {
            return _typeIdPairs.GetValueOrDefault(packet.GetType(), -1);
        }
    }
}
