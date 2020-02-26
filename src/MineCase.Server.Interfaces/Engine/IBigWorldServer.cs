using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.Engine
{
    public interface IBigWorldServer : IGrainWithIntegerKey
    {
        Task StartServer();

        Task StopServer();
    }
}
