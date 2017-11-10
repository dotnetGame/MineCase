using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MineCase.Client.Network.Login;
using MineCase.Protocol;
using MineCase.Protocol.Login;
using MineCase.Serialization;

namespace MineCase.Client.Network
{
    /// <summary>
    /// Login 状态的包路由
    /// </summary>
    internal partial class PacketRouter
    {
        private object DeserializeLoginPacket(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            object innerPacket;
            switch (packet.PacketId)
            {
                // Disconnect
                case 0x00:
                    innerPacket = LoginDisconnect.Deserialize(ref br);
                    break;

                // Login Success
                case 0x02:
                    innerPacket = LoginSuccess.Deserialize(ref br);
                    break;
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
            }

            if (!br.IsCosumed)
                throw new InvalidDataException($"Packet data is not fully consumed.");
            return innerPacket;
        }

        private Task DispatchPacket(LoginDisconnect packet)
        {
            var requestGrain = _sessionScope.ServiceProvider.Resolve<ILoginHandler>();
            requestGrain.OnDisconnect(packet);
            return Task.CompletedTask;
        }

        private Task DispatchPacket(LoginSuccess packet)
        {
            var requestGrain = _sessionScope.ServiceProvider.Resolve<ILoginHandler>();
            requestGrain.OnLoginSuccess(packet);
            return Task.CompletedTask;
        }
    }
}
