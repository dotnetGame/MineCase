using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Protocol.Login;
using System.Threading.Tasks;
using MineCase.Server.User;
using MineCase.Server.Game;

namespace MineCase.Server.Network.Login
{
    class LoginFlowGrain : Grain, ILoginFlow
    {
        private bool _useAuthentication = false;

        public async Task DispatchPacket(LoginStart packet)
        {
            if (_useAuthentication)
                throw new NotImplementedException();
            else
            {
                var user = await GrainFactory.GetGrain<INonAuthenticatedUser>(packet.Name).GetUser();
                var uuid = user.GetPrimaryKey();
                await SendLoginSuccess(packet.Name, uuid);
                
                await user.SetClientPacketSink(GrainFactory.GetGrain<IClientboundPacketSink>(this.GetPrimaryKey()));
                await GrainFactory.GetGrain<IPacketRouter>(this.GetPrimaryKey()).BindToUser(user);

                var world = await user.GetWorld();
                var game = GrainFactory.GetGrain<IGameSession>(world.GetPrimaryKeyString());
                await game.JoinGame(user);
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
    }
}
