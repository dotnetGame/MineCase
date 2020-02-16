using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using MineCase.Gateway.Network.Handler;
using MineCase.Gateway.Network.Handler.Handshaking;
using MineCase.Gateway.Network.Handler.Status;
using MineCase.Protocol.Handshaking;
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
        private INetHandler _packetHandler;

        private SessionState _sessionState = SessionState.Handshake;

        private readonly ActionBlock<ISerializablePacket> _outcomingPacketDispatcher;

        public ClientSession(TcpClient tcpClient, IClusterClient clusterClient)
        {
            _sessionId = Guid.NewGuid();
            _tcpClient = tcpClient;
            _client = clusterClient;

            _packetInfo = new PacketInfo();
            _encoder = new PacketEncoder(PacketDirection.ClientBound, _packetInfo);
            _decoder = new PacketDecoder(PacketDirection.ServerBound, _packetInfo);
            _packetHandler = new ServerHandshakeNetHandler(this);
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

        // Switch session state
        public void SetSessionState(SessionState state)
        {
            _sessionState = state;
        }

        public void SetNetHandler(SessionState state)
        {
            switch (state)
            {
                case SessionState.Login:
                    throw new NotImplementedException();
                    break;
                case SessionState.Status:
                    _packetHandler = new ServerStatusNetHandler(this);
                    break;
                case SessionState.Play:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new NotImplementedException("Invalid intention " + state.ToString());
            }
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
                    await _outcomingPacketDispatcher.Completion;
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

        private async Task SendOutcomingPacket(ISerializablePacket packet)
        {
            if (packet == null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Send);
                _outcomingPacketDispatcher.Complete();
            }
            else
            {
                RawPacket rawPacket = new RawPacket();
                using (MemoryStream ms = new MemoryStream())
                {
                    if (_useCompression)
                        _encoder.Encode(packet, ms);
                    else
                        _encoder.Encode(packet, ms);
                    rawPacket.RawData = ms.ToArray();
                    rawPacket.Length = rawPacket.RawData.Length;
                }

                System.Console.WriteLine($"Send packet id:{_packetInfo.GetPacketId(packet):x2}, length: {rawPacket.Length}");

                await rawPacket.SerializeAsync(_dataStream);
            }
        }

        // Process packets from game clients
        private async Task ProcessPacket()
        {
            // Read raw packet
            RawPacket rawPacket = new RawPacket();
            await rawPacket.DeserializeAsync(_dataStream);

            // Read packet
            ISerializablePacket packet = null;
            using (MemoryStream ms = new MemoryStream(rawPacket.RawData))
            {
                packet = _decoder.Decode((ProtocolType)_sessionState, ms);
            }

            switch (_sessionState)
            {
                case SessionState.Handshake:
                    await ProcessHandshakePacket(packet);
                    break;
                case SessionState.Login:
                    await ProcessLoginPacket(packet);
                    break;
                case SessionState.Play:
                    await ProcessPlayPacket(packet);
                    break;
                case SessionState.Status:
                    await ProcessStatusPacket(packet);
                    break;
                default:
                    throw new InvalidDataException($"Invalid session state.");
            }
        }

        private async Task ProcessStatusPacket(ISerializablePacket packet)
        {
            var handler = (IServerStatusNetHandler)_packetHandler;
            int packetId = _packetInfo.GetPacketId(packet);
            switch (packetId)
            {
                // request
                case 0x00:
                    await handler.ProcessRequest((Request)packet);
                    break;

                // ping
                case 0x01:
                    await handler.ProcessPing((Ping)packet);
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packetId:X2}.");
            }
        }

        private async Task ProcessHandshakePacket(ISerializablePacket packet)
        {
            var handler = (IHandshakeNetHandler)_packetHandler;
            int packetId = _packetInfo.GetPacketId(packet);
            switch (packetId)
            {
                // handshake
                case 0x00:
                    await handler.ProcessHandshake((Handshake)packet);
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packetId:X2}.");
            }
        }

        private Task ProcessLoginPacket(ISerializablePacket packet)
        {
            return Task.CompletedTask;
        }

        private Task ProcessPlayPacket(ISerializablePacket packet)
        {
            return Task.CompletedTask;
        }
    }
}
