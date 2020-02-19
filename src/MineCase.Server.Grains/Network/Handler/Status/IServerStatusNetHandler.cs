using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol.Status.Server;

namespace MineCase.Server.Network.Handler.Status
{
    public interface IServerStatusNetHandler : INetHandler
    {
        Task ProcessPing(Ping packetIn);

        Task ProcessRequest(Request packetIn);
    }
}
