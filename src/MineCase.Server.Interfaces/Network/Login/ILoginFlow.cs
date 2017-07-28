using MineCase.Protocol.Login;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MineCase.Server.Network.Login
{
    public interface ILoginFlow : IGrainWithGuidKey
    {
        Task DispatchPacket(LoginStart packet);
    }
}
