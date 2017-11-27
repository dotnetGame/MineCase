using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Game.Entities.Components;
using Orleans.Concurrency;

namespace MineCase.Server.Network.Play
{
    internal class ClientboundPacketComponent : Component, IHandle<BindToUser>, IHandle<KickPlayer>, IHandle<PacketForwardToPlayer>
    {
        private IClientboundPacketSink _sink;

        public ClientboundPacketComponent(string name = "clientboundPacket")
            : base(name)
        {
        }

        public ClientPlayPacketGenerator GetGenerator()
            => new ClientPlayPacketGenerator(_sink);

        public Task Kick()
        {
            return _sink.Close();
        }

        async Task IHandle<BindToUser>.Handle(BindToUser message)
        {
            _sink = await message.User.GetClientPacketSink();
        }

        Task IHandle<KickPlayer>.Handle(KickPlayer message)
        {
            return Kick();
        }

        Task IHandle<PacketForwardToPlayer>.Handle(PacketForwardToPlayer message)
        {
            return _sink.SendPacket(message.PacketId, message.Data.AsImmutable());
        }
    }
}
