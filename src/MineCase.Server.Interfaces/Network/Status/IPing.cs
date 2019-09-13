using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Status;
using Orleans;

namespace MineCase.Server.Network.Status
{
    public interface IPing : IGrainWithIntegerKey
    {
        Task DispatchPacket(Guid sessionId, Ping packet);
    }
}
