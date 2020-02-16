using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Protocol.Protocol.Status.Client;
using MineCase.Protocol.Protocol.Status.Server;

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
        private Dictionary<int, ProtocolType> _idProtocolTypePairs;
        private Dictionary<int, PacketDirection> _idPacketDirectionPairs;

        public PacketInfo()
        {
            _typeIdPairs.Add(typeof(Ping), 0x01);
        }

        public int GetPacketId(ISerializablePacket packet)
        {
            return _typeIdPairs.GetValueOrDefault(packet.GetType(), -1);
        }

        public PacketDirection GetPacketDirection(ISerializablePacket packet)
        {
            var id = GetPacketId(packet);
            return _idPacketDirectionPairs[id];
        }

        public ProtocolType GetProtocolType(ISerializablePacket packet)
        {
            var id = GetPacketId(packet);
            return _idProtocolTypePairs[id];
        }

        public ISerializablePacket GetPacket(PacketDirection direction, int id)
        {
            if (direction == PacketDirection.ClientBound)
            {
                switch (id)
                {
                    // Status
                    case 0x00:
                        return new Response();
                    case 0x01:
                        return new Pong();

                    // Handshake
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{id:X2}.");
                }
            }
            else if (direction == PacketDirection.ServerBound)
            {
                switch (id)
                {
                    // Status
                    case 0x00:
                        return new Request();
                    case 0x01:
                        return new Ping();

                    // Handshake
                    default:
                        throw new InvalidDataException($"Unrecognizable packet id: 0x{id:X2}.");
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid PacketDirection.");
            }
        }
    }
}
