using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MineCase.Gateway.Network.Handler.Status;
using MineCase.Protocol.Protocol;
using MineCase.Protocol.Protocol.Status.Server;
using Orleans;
using ProtocolType = MineCase.Protocol.Protocol.ProtocolType;

namespace MineCase.Gateway.Network
{
    public class ClientSession : IDisposable
    {
        private readonly Guid _sessionId;
        private TcpClient _tcpClient = null;
        private IClusterClient _client = null;
        private NetworkStream _dataStream = null;

        private volatile bool _useCompression = false;
        private uint _compressThreshold;

        private bool disposed = false;

        private PacketInfo _packetInfo;
        private PacketEncoder _encoder;
        private PacketDecoder _decoder;
        private IServerStatusNetHandler _statusHandler;

        private readonly ActionBlock<ISerializablePacket> _outcomingPacketDispatcher;

        public ClientSession(TcpClient tcpClient, IClusterClient clusterClient)
        {
            _sessionId = Guid.NewGuid();
            _tcpClient = tcpClient;
            _client = clusterClient;

            _packetInfo = new PacketInfo();
            _encoder = new PacketEncoder(PacketDirection.ClientBound, _packetInfo);
            _decoder = new PacketDecoder(PacketDirection.ServerBound, _packetInfo);
            _statusHandler = new ServerStatusNetHandler(this);
            _outcomingPacketDispatcher = new ActionBlock<ISerializablePacket>(SendOutcomingPacket);
        }

        ~ClientSession()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_tcpClient != null)
                {
                    _tcpClient.Dispose();
                    _tcpClient = null;
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            Dispose();
        }

        public void OnClosed()
        {
        }

        // Startup session
        public async Task Startup()
        {
            using (_dataStream = _tcpClient.GetStream())
            {
                try
                {
                    while (true)
                    {
                        await ProcessPacket();
                    }
                }
                catch (EndOfStreamException)
                {
                }
            }
        }

        // Send Packet to game clients
        public async Task SendPacket(ISerializablePacket packet)
        {
            try
            {
                if (!_outcomingPacketDispatcher.Completion.IsCompleted)
                    await _outcomingPacketDispatcher.SendAsync(packet);
            }
            catch
            {
                _outcomingPacketDispatcher.Complete();
            }
        }

        private Task SendOutcomingPacket(ISerializablePacket packet)
        {
            if (packet == null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Send);
                _outcomingPacketDispatcher.Complete();
            }
            else
            {
                if (_useCompression)
                {
                    _encoder.Encode(packet, _dataStream);
                }
                else
                {
                    _encoder.Encode(packet, _dataStream);
                }
            }

            return Task.CompletedTask;
        }

        // Process packets from game clients
        private async Task ProcessPacket()
        {
            ISerializablePacket packet = _decoder.Decode(_dataStream);
            var protocolType = _packetInfo.GetProtocolType(packet);
            if (protocolType == ProtocolType.Status)
            {
                await ProcessStatusPacket(packet);
            }
            else if (protocolType == ProtocolType.Handshake)
            {
                await ProcessHandshakePacket(packet);
            }
            else if (protocolType == ProtocolType.Login)
            {
                await ProcessLoginPacket(packet);
            }
            else if (protocolType == ProtocolType.Play)
            {
                await ProcessPlayPacket(packet);
            }
            else
            {
                throw new InvalidDataException($"Unrecognizable packet type.");
            }
        }

        private async Task ProcessStatusPacket(ISerializablePacket packet)
        {
            int packetId = _packetInfo.GetPacketId(packet);
            switch (packetId)
            {
                // request
                case 0x00:
                    await _statusHandler.ProcessRequest((Request)packet);
                    break;

                // ping
                case 0x01:
                    await _statusHandler.ProcessPing((Ping)packet);
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packetId:X2}.");
            }
        }

        private async Task ProcessHandshakePacket(ISerializablePacket packet)
        {

        }

        private async Task ProcessLoginPacket(ISerializablePacket packet)
        {

        }

        private async Task ProcessPlayPacket(ISerializablePacket packet)
        {

        }
    }
}
