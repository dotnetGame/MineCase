using System;
using System.Collections.Generic;
using System.Text;
using MineCase.Protocol;
using MineCase.Protocol.Protocol;
using Orleans;

namespace MineCase.Server.Network
{
    public interface IClientboundPacketObserver : IGrainObserver
    {
        void ReceivePacket(ISerializablePacket packet);

        void OnClosed();
    }
}
