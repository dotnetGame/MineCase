using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MineCase.Client.Network.Status;
using MineCase.Protocol;
using MineCase.Protocol.Status;
using MineCase.Serialization;

namespace MineCase.Client.Network
{
    /// <summary>
    /// Status 状态的包路由
    /// </summary>
    internal partial class PacketRouter
    {
        private object DeserializeStatusPacket(UncompressedPacket packet)
        {
            var br = new SpanReader(packet.Data);
            switch (packet.PacketId)
            {
                // Response
                case 0x00:
                    return Response.Deserialize(ref br);

                // Pong
                case 0x01:
                    return Pong.Deserialize(ref br);
                default:
                    throw new InvalidDataException($"Unrecognizable packet id: 0x{packet.PacketId:X2}.");
            }
        }

        private Task DispatchPacket(Response packet)
        {
            var requestGrain = _componentContext.Resolve<IStatusHandler>();
            requestGrain.OnResponse(_sessionId, packet);
            return Task.CompletedTask;
        }

        private Task DispatchPacket(Pong packet)
        {
            var requestGrain = _componentContext.Resolve<IStatusHandler>();
            requestGrain.OnPong(_sessionId, packet);
            return Task.CompletedTask;
        }
    }
}
