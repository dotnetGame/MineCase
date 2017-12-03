using System;
using System.Collections.Generic;
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
        private bool _useAuthentication = false;

        public async Task DispatchPacket(LoginStart packet)
        {
            if (_useAuthentication)
            {
                throw new NotImplementedException();
            }
            else
            {
                var user = await GrainFactory.GetGrain<INonAuthenticatedUser>(packet.Name).GetUser();
                var world = await user.GetWorld();
                var gameSession = GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());
                var settings = await GrainFactory.GetGrain<IServerSettings>(0).GetSettings();

                if (await user.GetProtocolVersion() > MineCase.Protocol.Protocol.Version)
                {
                    await SendLoginDisconnect("{\"text\":\"Outdated server!I'm still on 1.12\"}");
                }
                else if (await user.GetProtocolVersion() < MineCase.Protocol.Protocol.Version)
                {
                    await SendLoginDisconnect("{\"text\":\"Outdated client!Please use 1.12\"}");
                }
                else if (await gameSession.UserNumber() >= settings.MaxPlayers)
                {
                    await SendLoginDisconnect("{\"text\":\"The server is full!\"}");
                }
                else
                {
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
    }
}
