using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol;
using Orleans;

namespace MineCase.Server.Network
{
    public interface IClientboundPacketSink : IPacketSink, IGrainWithGuidKey
    {
        Task Subscribe(IClientboundPacketObserver observer);

        Task UnSubscribe(IClientboundPacketObserver observer);

        Task NotifyUseCompression(uint threshold);

        Task Close();
    }
}
