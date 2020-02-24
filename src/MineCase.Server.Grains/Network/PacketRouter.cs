using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol;
using MineCase.Protocol.Protocol.Handshaking.Server;
using MineCase.Protocol.Protocol.Login.Server;
using MineCase.Protocol.Protocol.Status.Server;
using MineCase.Server.Network.Handler;
using MineCase.Server.Network.Handler.Handshaking;
using MineCase.Server.Network.Handler.Login;
using MineCase.Server.Network.Handler.Play;
using MineCase.Server.Network.Handler.Status;
using MineCase.Game.Server.MultiPlayer;
using Orleans;

namespace MineCase.Server.Network
{
    public class PacketRouter : Grain, IPacketRouter
    {
        private IUser _user;
        private PacketInfo _packetInfo;
        private PacketDecoder _decoder;
        private INetHandler _packetHandler;

        private SessionState _sessionState = SessionState.Handshake;

        public override Task OnActivateAsync()
        {
            _packetInfo = new PacketInfo();
            _decoder = new PacketDecoder(PacketDirection.ServerBound, _packetInfo);
            _packetHandler = new ServerHandshakeNetHandler(
                GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey()),
                GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()),
                GrainFactory);
            return base.OnActivateAsync();
        }

        // Switch session state
        public Task SetSessionState(SessionState state)
        {
            _sessionState = state;
            return Task.CompletedTask;
        }

        public Task SetNetHandler(SessionState state)
        {
            switch (state)
            {
                case SessionState.Login:
                    _packetHandler = new ServerLoginNetHandler(
                        GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey()),
                        GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()),
                        GrainFactory);
                    break;
                case SessionState.Status:
                    _packetHandler = new ServerStatusNetHandler(
                        GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey()),
                        GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()),
                        GrainFactory);
                    break;
                case SessionState.Play:
                    _packetHandler = new ServerPlayNetHandler(
                        GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey()),
                        GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()),
                        GrainFactory,
                        _user);
                    break;
                default:
                    throw new NotImplementedException("Invalid intention " + state.ToString());
            }

            return Task.CompletedTask;
        }

        // Bind this router to user when user login
        public Task BindToUser(IUser user)
        {
            _user = user;
            return Task.CompletedTask;
        }

        // Process packets from game clients
        public async Task ProcessPacket(RawPacket rawPacket)
        {
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

        private async Task ProcessLoginPacket(ISerializablePacket packet)
        {
            var handler = (IServerLoginNetHandler)_packetHandler;
            int packetId = _packetInfo.GetPacketId(packet);
            switch (packetId)
            {
                // Login Start
                case 0x00:
                    await handler.ProcessLoginStart((LoginStart)packet);
                    break;

                // Encryption Response
                case 0x01:
                    await handler.ProcessEncryptionResponse((EncryptionResponse)packet);
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packetId:X2}.");
            }
        }

        private Task ProcessPlayPacket(ISerializablePacket packet)
        {
            var handler = (IServerPlayNetHandler)_packetHandler;
            int packetId = _packetInfo.GetPacketId(packet);
            switch (packetId)
            {
                // handshake
                case 0x00:
                    // await handler.ProcessHandshake((Handshake)packet);
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packetId:X2}.");
            }

            return Task.CompletedTask;
        }
    }
}
