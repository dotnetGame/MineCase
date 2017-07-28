using MineCase.Protocol.Status;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Status
{
    public interface IRequest : IGrainWithIntegerKey
    {
        Task DispatchPacket(Guid sessionId, Request packet);
    }
}
