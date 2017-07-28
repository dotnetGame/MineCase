using MineCase.Protocol.Status;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Status
{
    public interface IPing : IGrainWithIntegerKey
    {
        Task DispatchPacket(Guid sessionId, Ping packet);
    }
}
