using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using MineCase.Protocol.Protocol.Handshaking.Server;
using MineCase.Protocol.Protocol.Login.Client;
using MineCase.Protocol.Protocol.Login.Server;
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
        public PacketInfo()
        {
        }

        public int GetPacketId(ISerializablePacket packet)
        {
            var typeInfo = packet.GetType().GetTypeInfo();
            var attr = typeInfo.GetCustomAttribute<PacketAttribute>();
            return (int)attr.PacketId;
        }

        public PacketDirection GetPacketDirection(ISerializablePacket packet)
        {
            var typeInfo = packet.GetType().GetTypeInfo();
            var attr = typeInfo.GetCustomAttribute<PacketAttribute>();
            return attr.Direction;
        }

        public ProtocolType GetProtocolType(ISerializablePacket packet)
        {
            var typeInfo = packet.GetType().GetTypeInfo();
            var attr = typeInfo.GetCustomAttribute<PacketAttribute>();
            return attr.PacketType;
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
                        return GetPacketClientLogin(id);
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
                        return GetPacketServerLogin(id);
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

        public ISerializablePacket GetPacketServerLogin(int id)
        {
            switch (id)
            {
                case 0x00:
                    return new LoginStart();
                case 0x01:
                    return new EncryptionResponse();
                case 0x02:
                    return new LoginPluginResponse();
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{id:X2}.");
            }
        }

        public ISerializablePacket GetPacketClientLogin(int id)
        {
            switch (id)
            {
                case 0x00:
                    return new LoginDisconnect();
                case 0x01:
                    return new EncryptionRequest();
                case 0x02:
                    return new LoginSuccess();
                case 0x03:
                    return new SetCompression();
                case 0x04:
                    return new LoginPluginRequest();
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{id:X2}.");
            }
        }
    }
}
