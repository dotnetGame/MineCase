using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Status;

namespace MineCase.Client.Network.Status
{
    public interface IStatusHandler
    {
        void OnResponse(Guid sessionId, Response response);

        void OnPong(Guid sessionId, Pong pong);
    }

    internal class StatusHandler : IStatusHandler
    {
        void IStatusHandler.OnPong(Guid sessionId, Pong pong)
        {
            throw new NotImplementedException();
        }

        void IStatusHandler.OnResponse(Guid sessionId, Response response)
        {
            throw new NotImplementedException();
        }
    }
}
