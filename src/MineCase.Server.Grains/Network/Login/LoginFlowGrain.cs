using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Login;
using MineCase.Server.Game;
using MineCase.Server.Settings;
using MineCase.Server.User;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Login
{
    [Reentrant]
    internal class LoginFlowGrain : Grain, ILoginFlow
    {
        // private bool _useAuthentication = false;
        private const uint CompressPacketThreshold = 256;

        public async Task DispatchPacket(LoginStart packet)
        {
            var settingsGrain = GrainFactory.GetGrain<IServerSettings>(0);
            var settings = await settingsGrain.GetSettings();
            if (settings.OnlineMode)
            {
                // TODO auth and compression
                var user = GrainFactory.GetGrain<INonAuthenticatedUser>(packet.Name);

                if (await user.GetProtocolVersion() > MineCase.Protocol.Protocol.Version)
                {
                    await SendLoginDisconnect($"{{\"text\":\"Outdated server!I'm still on {Protocol.Protocol.VersionName}\"}}");
                }
                else if (await user.GetProtocolVersion() < MineCase.Protocol.Protocol.Version)
                {
                    await SendLoginDisconnect($"{{\"text\":\"Outdated client!Please use {Protocol.Protocol.VersionName}\"}}");
                }
                else
                {
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    Random verifyRandom = new Random();
                    Byte[] verifyToken = new byte[64];

                    verifyRandom.NextBytes(verifyToken);
                    var keys = rsa.ExportParameters(false);

                    await SendEncryptionRequest("", keys.Exponent, verifyToken);
                }
            }
            else
            {
                var nonAuthenticatedUser = GrainFactory.GetGrain<INonAuthenticatedUser>(packet.Name);

                if (await nonAuthenticatedUser.GetProtocolVersion() > MineCase.Protocol.Protocol.Version)
                {
                    await SendLoginDisconnect($"{{\"text\":\"Outdated server!I'm still on {Protocol.Protocol.VersionName}\"}}");
                }
                else if (await nonAuthenticatedUser.GetProtocolVersion() < MineCase.Protocol.Protocol.Version)
                {
                    await SendLoginDisconnect($"{{\"text\":\"Outdated client!Please use {Protocol.Protocol.VersionName}\"}}");
                }
                else
                {
                    // TODO refuse him if server is full
                    var user = await nonAuthenticatedUser.GetUser();
                    var world = await user.GetWorld();
                    var gameSession = GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());

                    await SendSetCompression();

                    var uuid = user.GetPrimaryKey();
                    await SendLoginSuccess(packet.Name, uuid);

                    await user.SetClientPacketSink(GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()));
                    var packetRouter = GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey());
                    await user.SetPacketRouter(packetRouter);
                    await packetRouter.BindToUser(user);

                    var game = GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());
                    await game.JoinGame(user);
                }
            }
        }

        public async Task DispatchPacket(EncryptionResponse packet)
        {
            var settingsGrain = GrainFactory.GetGrain<IServerSettings>(0);
            var settings = await settingsGrain.GetSettings();

            // TODO auth and compression
            var packetRouter = GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey());
            var userName = await packetRouter.GetUserName();

            // mojang request url
            var mojangURL = String.Format("https://sessionserver.mojang.com/session/minecraft/hasJoined?username={0}&serverId={1}&ip={2}", userName, "", settings.ServerIp);

            // success
            var user = await GrainFactory.GetGrain<INonAuthenticatedUser>(userName).GetUser();
            var world = await user.GetWorld();
            var gameSession = GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());

            var uuid = user.GetPrimaryKey();
            await SendLoginSuccess(userName, uuid);

            await user.SetClientPacketSink(GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()));
            await user.SetPacketRouter(packetRouter);

            var game = GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());
            await game.JoinGame(user);
        }

        private async Task SendSetCompression()
        {
            var sink = GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey());
            await sink.SendPacket(new SetCompression { Threshold = CompressPacketThreshold });
            await sink.NotifyUseCompression(CompressPacketThreshold);
        }

        private async Task SendLoginSuccess(string userName, Guid uuid)
        {
            var sink = GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey());
            await GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey()).Play();
            await sink.SendPacket(new LoginSuccess
            {
                Username = userName,
                UUID = uuid.ToString()
            });
        }

        private async Task SendLoginDisconnect(string reason)
        {
            var sink = GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey());
            await sink.SendPacket(new LoginDisconnect
            {
                Reason = reason
            });
        }

        private async Task SendEncryptionRequest(String serverID, byte[] publicKey, byte[] verifyToken)
        {
            var sink = GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey());
            await sink.SendPacket(new EncryptionRequest
            {
                ServerID = serverID,
                PublicKeyLength = (uint)publicKey.Length,
                PublicKey = publicKey,
                VerifyTokenLength = (uint)verifyToken.Length,
                VerifyToken = verifyToken
            });
        }
    }
}
