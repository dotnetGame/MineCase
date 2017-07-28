using MineCase.Protocol;
using MineCase.Protocol.Handshaking;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network
{
    public interface IClientboundPaketSink : IGrainWithGuidKey
    {
        Task Subscribe(IClientboundPacketObserver observer);
        Task UnSubscribe(IClientboundPacketObserver observer);
        
        Task SendPacket(ISerializablePacket packet);
    }
}
