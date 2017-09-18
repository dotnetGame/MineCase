using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MineCase.Server.Game.Entities;
using Orleans.Concurrency;

namespace MineCase.Server.World
{
    public interface IWorldPartition : IAddressByPartition, ITickObserver
    {
        Task Enter(IPlayer player);

        Task Leave(IPlayer player);
    }
}
