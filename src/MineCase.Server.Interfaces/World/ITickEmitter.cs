using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace MineCase.Server.World
{
    public interface ITickEmitter : IAddressByPartition
    {
        Task Start();

        Task Stop();
    }
}
