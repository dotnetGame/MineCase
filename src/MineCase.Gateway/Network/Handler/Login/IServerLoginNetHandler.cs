using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Protocol.Protocol.Login.Server;

namespace MineCase.Gateway.Network.Handler.Login
{
    public interface IServerLoginNetHandler : INetHandler
    {
        Task ProcessLoginStart(LoginStart packet);

        Task ProcessEncryptionResponse(EncryptionResponse packet);
    }
}
