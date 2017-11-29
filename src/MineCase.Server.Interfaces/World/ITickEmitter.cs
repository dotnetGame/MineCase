using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Game.Entities;
using MineCase.World;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    public interface ITickEmitter : IAddressByPartition
    {
        Task Subscribe(IDependencyObject observer);

        Task Unsubscribe(IDependencyObject observer);
    }
}
