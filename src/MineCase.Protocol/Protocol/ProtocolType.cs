using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MineCase.Protocol.Handshaking;
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
        private Dictionary<Type, int> _typeIdPairs = new Dictionary<Type, int>();
        private Dictionary<Type, ProtocolType> _idProtocolTypePairs = new Dictionary<Type, ProtocolType>();
        private Dictionary<Type, PacketDirection> _idPacketDirectionPairs = new Dictionary<Type, PacketDirection>();

        public PacketInfo()
        {
            _typeIdPairs.Add(typeof(Request), 0x00);
            _idProtocolTypePairs.Add(typeof(Request), ProtocolType.Status);
            _idPacketDirectionPairs.Add(typeof(Request), PacketDirection.ServerBound);

            _typeIdPairs.Add(typeof(Response), 0x00);
            _idProtocolTypePairs.Add(typeof(Response), ProtocolType.Status);
            _idPacketDirectionPairs.Add(typeof(Response), PacketDirection.ClientBound);

            _typeIdPairs.Add(typeof(Ping), 0x01);
            _idProtocolTypePairs.Add(typeof(Ping), ProtocolType.Status);
            _idPacketDirectionPairs.Add(typeof(Ping), PacketDirection.ServerBound);

            _typeIdPairs.Add(typeof(Pong), 0x01);
            _idProtocolTypePairs.Add(typeof(Pong), ProtocolType.Status);
            _idPacketDirectionPairs.Add(typeof(Pong), PacketDirection.ClientBound);

            _typeIdPairs.Add(typeof(Handshake), 0x00);
            _idProtocolTypePairs.Add(typeof(Handshake), ProtocolType.Handshake);
            _idPacketDirectionPairs.Add(typeof(Handshake), PacketDirection.ServerBound);
        }

        public int GetPacketId(ISerializablePacket packet)
        {
            return _typeIdPairs.GetValueOrDefault(packet.GetType(), -1);
        }

        public PacketDirection GetPacketDirection(ISerializablePacket packet)
        {
            return _idPacketDirectionPairs[packet.GetType()];
        }

        public ProtocolType GetProtocolType(ISerializablePacket packet)
        {
            return _idProtocolTypePairs[packet.GetType()];
        }

        public ISerializablePacket GetPacket(PacketDirection direction, ProtocolType protocolType, int id)
        {
            if (direction == PacketDirection.ClientBound)
            {
                switch (protocolType)
                {
                    case ProtocolType.Handshake:
                        return null;
                    case ProtocolType.Login:
                        return null;
                    case ProtocolType.Play:
                        return null;
                    case ProtocolType.Status:
                        return GetPacketClientStatus(id);
                    default:
                        throw new InvalidDataException("GetPacket: Invalid protocol type.");
                }
            }
            else if (direction == PacketDirection.ServerBound)
            {
                switch (protocolType)
                {
                    case ProtocolType.Handshake:
                        return GetPacketServerHandshake(id);
                    case ProtocolType.Login:
                        return null;
                    case ProtocolType.Play:
                        return null;
                    case ProtocolType.Status:
                        return GetPacketServerStatus(id);
                    default:
                        throw new InvalidDataException("GetPacket: Invalid protocol type.");
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid PacketDirection.");
            }
        }

        public ISerializablePacket GetPacketServerHandshake(int id)
        {
            switch (id)
            {
                // Handshake
                case 0x00:
                    return new Handshake();
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{id:X2}.");
            }
        }

        public ISerializablePacket GetPacketClientStatus(int id)
        {
            switch (id)
            {
                // Status
                case 0x00:
                    return new Response();
                case 0x01:
                    return new Pong();
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{id:X2}.");
            }
        }

        public ISerializablePacket GetPacketServerStatus(int id)
        {
            switch (id)
            {
                // Status
                case 0x00:
                    return new Request();
                case 0x01:
                    return new Ping();
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{id:X2}.");
            }
        }
    }
}
