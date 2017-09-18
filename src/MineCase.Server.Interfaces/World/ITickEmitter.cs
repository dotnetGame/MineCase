using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    public interface ITickEmitter : IAddressByPartition
    {
        [OneWay]
        Task OnGameTick(TimeSpan deltaTime, long worldAge);

        Task Subscribe(ITickObserver observer);

        Task Unsubscribe(ITickObserver observer);
    }

    public interface ITickObserver : IGrainObserver
    {
        void OnGameTick(TimeSpan deltaTime, long worldAge);
    }
}
