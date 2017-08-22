using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using MineCase.Protocol.Handshaking;
using Orleans;

namespace MineCase.Server.Network
{
    public interface IClientboundPacketSink : IGrainWithGuidKey
    {
        Task Subscribe(IClientboundPacketObserver observer);

        Task UnSubscribe(IClientboundPacketObserver observer);

        Task SendPacket(ISerializablePacket packet);

        Task Close();
    }
}
