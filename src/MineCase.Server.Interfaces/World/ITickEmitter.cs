using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Engine;
using MineCase.Server.Game.Entities;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    public interface ITickEmitter : IAddressByPartition
    {
        [OneWay]
        Task OnGameTick(TimeSpan deltaTime, long worldAge);

        Task Subscribe(IDependencyObject observer);

        Task Unsubscribe(IDependencyObject observer);
    }
}
