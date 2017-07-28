using MineCase.Protocol;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;

namespace MineCase.Server.Network
{
    public interface IClientboundPacketObserver : IGrainObserver
    {
        void ReceivePacket(UncompressedPacket packet);
    }
}
