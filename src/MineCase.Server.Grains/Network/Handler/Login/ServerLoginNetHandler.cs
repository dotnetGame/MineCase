using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Game;
using MineCase.Protocol.Protocol;
using MineCase.Protocol.Protocol.Login.Client;
using MineCase.Protocol.Protocol.Login.Server;
using MineCase.Server.Server;
using Orleans;

namespace MineCase.Server.Network.Handler.Login
{
    public enum LoginState
    {
        Hello,
        Key,
        Authenticating,
        ReadyToAccept,
        DelayAccept,
        Accepted,
    }

    public class ServerLoginNetHandler : IServerLoginNetHandler
    {
        private IPacketRouter _clientSession;

        private IClientboundPacketSink _packetSink;

        private IGrainFactory _client;

        private LoginState _loginState;

        private GameProfile _gameProfile;

        public ServerLoginNetHandler(IPacketRouter session, IClientboundPacketSink packetSink, IGrainFactory client)
        {
            _clientSession = session;
            _packetSink = packetSink;
            _client = client;
            _loginState = LoginState.Hello;
            _gameProfile = new GameProfile();
        }

        public Task ProcessEncryptionResponse(EncryptionResponse packet)
        {
            throw new NotImplementedException();
        }

        public async Task ProcessLoginStart(LoginStart packet)
        {
            if (_loginState != LoginState.Hello)
                throw new InvalidOperationException("ProcessLoginStart: Invalid login state.");
            _gameProfile = new GameProfile(null, packet.Name);
            var server = _client.GetGrain<IMinecraftServer>("Default");

            if (await server.GetOnlineMode())
            {
                throw new NotImplementedException("ProcessLoginStart: Online mode is not implemented.");
            }
            else
            {
                _loginState = LoginState.ReadyToAccept;
                await TryAcceptPlayer();
            }
        }

        private async Task TryAcceptPlayer()
        {
            if (!_gameProfile.Complete)
            {
                // Offline profile
                _gameProfile = new GameProfile(Guid.NewGuid().ToString(), _gameProfile.Name);
            }

            _loginState = LoginState.Accepted;
            var server = _client.GetGrain<IMinecraftServer>("Default");
            if (await server.GetNetworkCompressionThreshold() >= 0)
            {
                throw new NotImplementedException("TryAcceptPlayer: Packet compression is not implemented.");
            }

            // Send success packet
            LoginSuccess successPacket = new LoginSuccess
            {
                UUID = _gameProfile.UUID,
                Username = _gameProfile.Name,
            };

            await _packetSink.SendPacket(successPacket);

            // Change session state
            await _clientSession.SetSessionState(SessionState.Play);

            // Set net handler
            await _clientSession.SetNetHandler(SessionState.Play);

            MineCase.Protocol.Protocol.Play.Client.ChunkData chunkData = new MineCase.Protocol.Protocol.Play.Client.ChunkData();
            await _packetSink.SendPacket(chunkData);
        }
    }
}
